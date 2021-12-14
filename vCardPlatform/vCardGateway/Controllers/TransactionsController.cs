using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class TransactionsController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;
        
        private string entitiesPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Entities.xml";
        RestClient client = new RestClient("http://localhost:59458/api");

        [Route("api/transactions")]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            try
            {

                HandlerXML handlerXML = new HandlerXML(entitiesPath);

                #region Find Debit Entity
                Entity entityDebit = handlerXML.GetEntityByName(transaction.FromEntity);

                if (entityDebit == null)
                {
                    return NotFound();
                }
                #endregion

                #region Find Credit Entity
                Entity entityCredit = handlerXML.GetEntityByName(transaction.Payment_Type);

                if (entityCredit == null)
                {
                    return NotFound();
                }
                #endregion

                #region Create Clients
                RestClient clientDebit = new RestClient(entityDebit.Endpoint + "/api");
                RestClient clientCredit = new RestClient(entityCredit.Endpoint + "/api");
                #endregion

                #region Debit Request
                RestRequest requestDebit = new RestRequest("/transactions", Method.POST, DataFormat.Json);
                requestDebit.AddJsonBody(new { 
                    payment_reference=transaction.Payment_Reference, 
                    value=transaction.Value, 
                    description=transaction.Description,
                    payment_type=transaction.Payment_Type,
                    category=transaction.Category,
                    type=transaction.Type.ToString(),
                });
                requestDebit.AddHeader("Authorization", Request.Headers.Authorization.ToString());
                IRestResponse<Transaction> responseDebit = clientDebit.Execute<Transaction>(requestDebit);
                #endregion

                #region Create Debit Transaction Log
                TransactionLog debitTransactionLog = new TransactionLog
                {
                    FromUser = transaction.FromUser,
                    FromEntity = transaction.FromEntity,
                    ToUser = transaction.Payment_Reference,
                    ToEntity = transaction.Payment_Type,
                    Amount = transaction.Value,
                };
                #endregion

                //If Debit Was Sucessfull We Try Cedit Transaction
                if (responseDebit.IsSuccessful)
                {
                    #region Post Debit Transaction Log
                    dynamic dataTransactionDebit = JsonConvert.DeserializeObject(responseDebit.Content);
                    if (dataTransactionDebit.data != null)
                        dataTransactionDebit = dataTransactionDebit.data;
                    Transaction debitTransaction = new Transaction
                    {
                        //Este operador '??' é para prevenir diferentes dados de diferentes fontes
                        OldBalance = dataTransactionDebit.old_balance ?? dataTransactionDebit.OldBalance,
                        NewBalance = dataTransactionDebit.new_balance ?? dataTransactionDebit.OldBalance,
                    };

                    debitTransactionLog.Status = "completed";
                    debitTransactionLog.Message = transaction.Type.ToString();
                    debitTransactionLog.Timestamp = DateTime.Now;

                    debitTransactionLog.NewBalance = debitTransaction.NewBalance;
                    debitTransactionLog.OldBalance = debitTransaction.OldBalance;

                    TransactionLogsController.PostTransactionLog(debitTransactionLog);
                    #endregion

                    #region Create Credit Transaction Log
                    TransactionLog creditTransactionLog = new TransactionLog
                    {
                        FromUser = transaction.Payment_Reference,
                        FromEntity = transaction.Payment_Type,
                        ToUser = transaction.FromUser,
                        ToEntity = transaction.FromEntity,
                        Amount = transaction.Value,
                    };
                    #endregion

                    #region Credit Request When Debit Succefull
                    RestRequest requestCredit = new RestRequest("/transactions", Method.POST, DataFormat.Json);
                    requestCredit.AddJsonBody(new
                    {
                        phonenumber = transaction.Payment_Reference.ToString(),
                        paymentreference = transaction.FromUser,
                        value = transaction.Value,
                        paymenttype = transaction.FromEntity,
                        type = "C",
                    });

                    requestCredit.AddHeader("Authorization", entityCredit.Authentication.Token);
                    IRestResponse<Transaction> responseCredit = clientCredit.Execute<Transaction>(requestCredit);
                    #endregion

                    //If Credit Was Sucessfull
                    if (responseCredit.IsSuccessful)
                    {
                        #region Post Credit Transaction Log
                        dynamic dataTransactionCredit = JsonConvert.DeserializeObject(responseCredit.Content);
                        if (dataTransactionCredit.data != null)
                            dataTransactionCredit = dataTransactionCredit.data;
                        Transaction creditTransaction = new Transaction
                        {
                            //Este operador '??' é para prevenir diferentes dados de diferentes fontes
                            OldBalance = dataTransactionCredit.old_balance ?? dataTransactionCredit.OldBalance,
                            NewBalance = dataTransactionCredit.new_balance ?? dataTransactionCredit.NewBalance,
                        };

                        creditTransactionLog.Status = "completed";
                        creditTransactionLog.Message = "C";
                        creditTransactionLog.Timestamp = DateTime.Now;

                        creditTransactionLog.NewBalance = creditTransaction.NewBalance;
                        creditTransactionLog.OldBalance = creditTransaction.OldBalance;

                        TransactionLogsController.PostTransactionLog(creditTransactionLog);
                        #endregion

                        //Send Earning Percentage To Costumer
                        #region Send Earning Percentage To Costumer

                        #region Post Earning Back Transaction
                        RestRequest requestEarningBack = new RestRequest("/transactions", Method.POST, DataFormat.Json);

                        requestEarningBack.AddJsonBody(new
                        {
                            vcard = transaction.FromUser,
                            payment_reference = entityDebit.Authentication.Username,
                            value = Math.Round(transaction.Value * (entityDebit.EarningPercentage / 100),2),
                            payment_type = "MBWAY",
                            type = "C",
                        });

                        requestEarningBack.AddHeader("Authorization", entityDebit.Authentication.Token);

                        IRestResponse<Transaction> responseEarningBack = clientDebit.Execute<Transaction>(requestEarningBack);
                        #endregion

                        #region Create Earning Back Transaction Log
                        TransactionLog earningBackTransactionLog = new TransactionLog
                        {
                            FromUser = entityDebit.Authentication.Username,
                            FromEntity = "MBWAY",
                            ToUser = transaction.FromUser,
                            ToEntity = transaction.FromEntity,
                            Amount = Math.Round(transaction.Value * (entityDebit.EarningPercentage / 100), 2),
                        };
                        #endregion

                        if (responseEarningBack.IsSuccessful)
                        {
                            #region Post Earning Back Transaction Log
                            dynamic dataTransactionEarningBack = JsonConvert.DeserializeObject(responseEarningBack.Content);
                            if (dataTransactionEarningBack.data != null)
                                dataTransactionEarningBack = dataTransactionEarningBack.data;
                            Transaction earningBackTransaction = new Transaction
                            {
                                //Este operador '??' é para prevenir diferentes dados de diferentes fontes
                                OldBalance = dataTransactionEarningBack.old_balance ?? dataTransactionEarningBack.OldBalance,
                                NewBalance = dataTransactionEarningBack.new_balance ?? dataTransactionEarningBack.OldBalance,
                            };

                            earningBackTransactionLog.Status = "Earning Back money sent back";
                            earningBackTransactionLog.Message = $"Earning percentage of {entityDebit.EarningPercentage}%";
                            earningBackTransactionLog.Timestamp = DateTime.Now;

                            earningBackTransactionLog.NewBalance = earningBackTransaction.NewBalance;
                            earningBackTransactionLog.OldBalance = earningBackTransaction.OldBalance;

                            TransactionLogsController.PostTransactionLog(earningBackTransactionLog);
                            #endregion
                        }
                        else
                        {
                            #region Post Error Earning Back Transaction Log
                            earningBackTransactionLog.Status = "ERROR, Earning Back money WAS NOT sent back";
                            earningBackTransactionLog.ErrorMessage = responseEarningBack.Content;
                            earningBackTransactionLog.Message = responseCredit.Content.ToString();
                            earningBackTransactionLog.Timestamp = DateTime.Now;

                            TransactionLogsController.PostTransactionLog(earningBackTransactionLog);
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        //SE A TRANSAÇÃO FALHAR TEMOS DE DEVOLVER O DINHEIRO AO COSTUMER QUE EFETUOU A TRANSFERÊNCIA
                        //COM AS CREDENCIAIS DO GATEWAY 
                        #region Send Money Back To Costumer
                        RestRequest requestCreditBack = new RestRequest("/transactions", Method.POST, DataFormat.Json);

                        requestCreditBack.AddJsonBody(new
                        {
                            vcard = transaction.FromUser,
                            payment_reference = entityDebit.Authentication.Username,
                            value = transaction.Value,
                            payment_type = "MBWAY",
                            type = "C",
                        });

                        requestCreditBack.AddHeader("Authorization", entityDebit.Authentication.Token);

                        IRestResponse<Transaction> responseCreditBack = clientDebit.Execute<Transaction>(requestCreditBack);
                        #endregion

                        if (responseCreditBack.IsSuccessful)
                        {
                            #region Post Credit Back Transaction Log
                            dynamic dataTransactionCreditBack = JsonConvert.DeserializeObject(responseCreditBack.Content);
                            if (dataTransactionCreditBack.data != null)
                                dataTransactionCreditBack = dataTransactionCreditBack.data;
                            Transaction creditTransaction = new Transaction
                            {
                                //Este operador '??' é para prevenir diferentes dados de diferentes fontes
                                OldBalance = dataTransactionCreditBack.old_balance ?? dataTransactionCreditBack.OldBalance,
                                NewBalance = dataTransactionCreditBack.new_balance ?? dataTransactionCreditBack.OldBalance,
                            };

                            creditTransactionLog.Status = "failed, money sent back";
                            creditTransactionLog.ErrorMessage = responseCredit.Content;
                            creditTransactionLog.Message = "C";
                            creditTransactionLog.Timestamp = DateTime.Now;

                            creditTransactionLog.NewBalance = creditTransaction.NewBalance;
                            creditTransactionLog.OldBalance = creditTransaction.OldBalance;

                            TransactionLogsController.PostTransactionLog(creditTransactionLog);
                            #endregion
                        }
                        else
                        {
                            #region Post Error Credit Back Transaction Log
                            creditTransactionLog.Status = "ERROR, money WAS NOT sent back";
                            creditTransactionLog.ErrorMessage = "Response Credit: " + responseCredit.Content + "\n Response Credit Back" + responseCreditBack.Content;
                            creditTransactionLog.Message = responseCredit.Content.ToString();
                            creditTransactionLog.Timestamp = DateTime.Now;

                            TransactionLogsController.PostTransactionLog(creditTransactionLog);
                            #endregion
                        }
                        return InternalServerError(new Exception(responseCredit.StatusCode.ToString()));
                    }

                    return Ok(JsonConvert.DeserializeObject(responseDebit.Content));
                }
                else
                {
                    #region Post Error Debit Transaction Log
                    debitTransactionLog.Status = "ERROR, money WAS NOT sent";
                    debitTransactionLog.Message = responseDebit.Content.ToString();
                    debitTransactionLog.Timestamp = DateTime.Now;

                    TransactionLogsController.PostTransactionLog(debitTransactionLog);
                    #endregion

                    return BadRequest(responseDebit.Content.ToString());
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }
    }
}

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

        /// <summary>
        /// Search for all transactions based on User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <returns>Transaction found</returns>
        /// <response code="200">Returns the Transactions found. Returns null if you are not authorized</response>
        [Route("api/entities/{entity_id}/transactions")]
        public IHttpActionResult GetTransactions(string entity_id)
        {
            HandlerXML handlerXML = new HandlerXML(entitiesPath);
            DateTime responseTimeStart = DateTime.Now;
            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest("transactions", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<List<Transaction>> response = client.Execute<List<Transaction>>(request);
                dynamic dataDefaultTransaction = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultTransaction != null)
                {
                    GeneralLogsController.PostGeneralLog("Transaction", "N/A", entity.Name, response.StatusCode.ToString(), "GetTransactions", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(response.StatusCode, dataDefaultTransaction);
                }
                GeneralLogsController.PostGeneralLog("Transaction", "N/A", entity.Name, HttpStatusCode.Unauthorized.ToString(), "GetTransactions", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return Content(HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Search for a transaction based on given ID and User authenticated
        /// </summary>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="transaction_id">Transaction ID</param>
        /// <returns>A list of all transactions</returns>
        /// <response code="200">Returns the Transaction found</response>
        /// <response code="401">Transaction does not belongs to authenticated user</response>
        /// <response code="404">If the Transaction was not found</response>
        [Route("api/entities/{entity_id}/transactions/{transaction_id}")]
        public IHttpActionResult GetTransaction(string entity_id, int transaction_id)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"transactions/{transaction_id}", Method.GET);

                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<Transaction> response = client.Execute<Transaction>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                dynamic dataDefaultTransaction = JsonConvert.DeserializeObject(response.Content);
                if (dataDefaultTransaction != null)
                {
                    GeneralLogsController.PostGeneralLog("Transaction", "N/A", entity.Name, response.StatusCode.ToString(), "GetTransaction", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(response.StatusCode, dataDefaultTransaction);
                }
                GeneralLogsController.PostGeneralLog("Transaction", "N/A", entity.Name, response.StatusCode.ToString(), "GetTransaction", response.StatusDescription, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return Content(response.StatusCode, response.StatusDescription);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert Transaction from authenticated User to other User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "Payment_Reference": "913406043",
        ///         "Value": "3",
        ///         "Payment_Type": "VCARD",
        ///         "Type": "D",
        ///         "Category": "7",
        ///         "Description": "Food For School",
        ///         "FromUser": "900000001",
        ///         "FromEntity": "MBWAY" 
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="transaction">Transaction to insert</param>
        /// <returns>Transaction inserted</returns>
        /// <response code="200">Returns the newly created Transaction</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/transactions")]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            DateTime responseTimeStart = DateTime.Now;
            try
            {
                if (transaction == null || transaction.Payment_Reference == null || transaction.Payment_Reference == ""
                    || transaction.Value <= 0
                    || transaction.Payment_Type == null || transaction.Payment_Type == ""
                    || (transaction.Type != "D" && transaction.Type != "C")
                    || transaction.FromUser == null || transaction.FromUser == ""
                    || transaction.FromEntity == null || transaction.FromEntity == "")
                {
                    GeneralLogsController.PostGeneralLog("Transaction", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PostTransaction", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                HandlerXML handlerXML = new HandlerXML(entitiesPath);

                #region Find Debit Entity
                Entity entityDebit = handlerXML.GetEntityByName(transaction.FromEntity);

                if (entityDebit == null)
                {
                    GeneralLogsController.PostGeneralLog("Transaction", "N/A", "Gateway", HttpStatusCode.NotFound.ToString(), $"Failed to Find Entity {transaction.FromEntity}", "Invalid Entity", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.NotFound, $"{transaction.FromEntity} entity does not exist.");
                }
                #endregion

                #region Verify Max Limit Of Entity
                if (transaction.Value > entityDebit.MaxLimit)
                {
                    return BadRequest($"Value of transaction invalid, need to be lower than {entityDebit.MaxLimit.ToString()}");
                }
                #endregion

                #region Find Credit Entity
                Entity entityCredit = handlerXML.GetEntityByName(transaction.Payment_Type);

                if (entityCredit == null)
                {
                    GeneralLogsController.PostGeneralLog("Transaction", "N/A", "Gateway", HttpStatusCode.NotFound.ToString(), $"Failed to Find Entity {transaction.Payment_Type}", "Invalid Entity", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.NotFound, $"{transaction.Payment_Type} entity does not exist.");
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
                    category=transaction.Category == 0 ? "" : transaction.Category.ToString(),
                    type=transaction.Type.ToString(),
                    phonenumber = transaction.FromUser.ToString() ?? null,
                });
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                requestDebit.AddHeader("Authorization", auth);
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
                    Type = "D"
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
                        Old_Balance = dataTransactionDebit.old_balance ?? dataTransactionDebit.Old_Balance,
                        New_Balance = dataTransactionDebit.new_balance ?? dataTransactionDebit.Old_Balance,
                    };

                    debitTransactionLog.Status = "completed";
                    debitTransactionLog.Message = transaction.Type.ToString();
                    debitTransactionLog.Timestamp = DateTime.Now;

                    debitTransactionLog.NewBalance = debitTransaction.New_Balance;
                    debitTransactionLog.OldBalance = debitTransaction.Old_Balance;

                    GeneralLogsController.PostGeneralLog("Transaction", debitTransactionLog.FromUser, debitTransactionLog.FromEntity, responseDebit.StatusCode.ToString(), $"Money [{debitTransactionLog.FromUser} FROM {debitTransactionLog.FromEntity} - {debitTransactionLog.Amount} €] WAS sent [D]", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
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
                        Type = "C"
                    };
                    #endregion

                    #region Credit Request When Debit Succefull
                    RestRequest requestCredit = new RestRequest("/transactions", Method.POST, DataFormat.Json);
                    requestCredit.AddJsonBody(new
                    {
                        vcard = transaction.Payment_Reference.ToString(),
                        phonenumber = transaction.Payment_Reference.ToString(),
                        payment_reference = transaction.FromUser,
                        value = transaction.Value,
                        payment_type = transaction.FromEntity,
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
                            Old_Balance = dataTransactionCredit.old_balance ?? dataTransactionCredit.Old_Balance,
                            New_Balance = dataTransactionCredit.new_balance ?? dataTransactionCredit.New_Balance,
                        };

                        creditTransactionLog.Status = "completed";
                        creditTransactionLog.Message = "C";
                        creditTransactionLog.Timestamp = DateTime.Now;

                        creditTransactionLog.NewBalance = creditTransaction.New_Balance;
                        creditTransactionLog.OldBalance = creditTransaction.Old_Balance;

                        TransactionLogsController.PostTransactionLog(creditTransactionLog);
                        #endregion

                        //Send Earning Percentage To Costumer
                        #region Send Earning Percentage To Costumer

                        #region Post Earning Back Transaction
                        RestRequest requestEarningBack = new RestRequest("/transactions", Method.POST, DataFormat.Json);

                        requestEarningBack.AddJsonBody(new
                        {
                            phonenumber = transaction.FromUser.ToString(),
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
                            Type = "E"
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
                                Old_Balance = dataTransactionEarningBack.old_balance ?? dataTransactionEarningBack.Old_Balance,
                                New_Balance = dataTransactionEarningBack.new_balance ?? dataTransactionEarningBack.Old_Balance,
                            };

                            earningBackTransactionLog.Status = "Earning Back money sent back";
                            earningBackTransactionLog.Message = $"Earning percentage of {entityDebit.EarningPercentage}%";
                            earningBackTransactionLog.Timestamp = DateTime.Now;

                            earningBackTransactionLog.NewBalance = earningBackTransaction.New_Balance;
                            earningBackTransactionLog.OldBalance = earningBackTransaction.Old_Balance;

                            GeneralLogsController.PostGeneralLog("Transaction", earningBackTransactionLog.ToUser, earningBackTransactionLog.ToEntity, responseEarningBack.StatusCode.ToString(), $"Earning Back money [{earningBackTransactionLog.ToUser} FROM {earningBackTransactionLog.ToEntity} - {earningBackTransactionLog.Amount} €] WAS sent back [E]", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
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

                            GeneralLogsController.PostGeneralLog("Transaction", earningBackTransactionLog.ToUser, earningBackTransactionLog.ToEntity, responseEarningBack.StatusCode.ToString(), $"Earning Back money [{earningBackTransactionLog.ToUser} FROM {earningBackTransactionLog.ToEntity} - {earningBackTransactionLog.Amount} €] WAS NOT sent back [E]", "Earning Back money WAS NOT sent back", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
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
                                Old_Balance = dataTransactionCreditBack.old_balance ?? dataTransactionCreditBack.Old_Balance,
                                New_Balance = dataTransactionCreditBack.new_balance ?? dataTransactionCreditBack.New_Balance,
                            };

                            creditTransactionLog.Status = "failed, money sent back";
                            creditTransactionLog.ErrorMessage = responseCredit.Content;
                            creditTransactionLog.Message = "C";
                            creditTransactionLog.Timestamp = DateTime.Now;

                            creditTransactionLog.NewBalance = creditTransaction.New_Balance;
                            creditTransactionLog.OldBalance = creditTransaction.Old_Balance;

                            GeneralLogsController.PostGeneralLog("Transaction", creditTransactionLog.ToUser, creditTransactionLog.ToEntity, responseCreditBack.StatusCode.ToString(), $"Money [{creditTransactionLog.ToUser} FROM {creditTransactionLog.ToEntity} - {creditTransactionLog.Amount} €] WAS sent back [C]", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
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

                            GeneralLogsController.PostGeneralLog("Transaction", creditTransactionLog.ToUser, creditTransactionLog.ToEntity, responseCreditBack.StatusCode.ToString(), $"Money [{creditTransactionLog.ToUser} FROM {creditTransactionLog.ToEntity} - {creditTransactionLog.Amount} €] WAS NOT sent back [C]", "! Money WAS NOT sent back", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                            TransactionLogsController.PostTransactionLog(creditTransactionLog);
                            #endregion
                        }
                        GeneralLogsController.PostGeneralLog("Transaction", creditTransactionLog.FromUser, creditTransactionLog.FromEntity, responseCreditBack.StatusCode.ToString(), $"Money [{creditTransactionLog.FromUser} FROM {creditTransactionLog.FromEntity} - {creditTransactionLog.Amount} €] WAS NOT sent [C]", "! Money WAS NOT sent", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));

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

                    GeneralLogsController.PostGeneralLog("Transaction", debitTransactionLog.FromUser, debitTransactionLog.FromEntity, responseDebit.StatusCode.ToString(), $"Money [{debitTransactionLog.FromUser} FROM {debitTransactionLog.FromEntity} - {debitTransactionLog.Amount} €] WAS NOT sent [D]", "! Money WAS NOT sent", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    TransactionLogsController.PostTransactionLog(debitTransactionLog);
                    #endregion

                    return BadRequest(responseDebit.Content.ToString());
                }

            }
            catch (Exception ex)
            {
                GeneralLogsController.PostGeneralLog("Transaction", null, "Gateway", HttpStatusCode.InternalServerError.ToString(), $"Fatal error", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return InternalServerError(new Exception(ex.Message));
            }
        }

        /// <summary>
        /// Update Transaction of authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///         "Category": "61",
        ///         "Description": "Food",
        ///     }
        ///     
        /// </remarks>
        /// <param name="entity_id">Entity ID</param>
        /// <param name="transaction_id">Transaction ID</param>
        /// <param name="transaction">Transaction to be updated</param>
        /// <returns>Transaction Updated</returns>
        /// <response code="200">Returns the updated created Transaction</response>
        /// <response code="401">Transaction does not belongs to authenticated user</response>
        /// <response code="404">If given Transaction not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/entities/{entity_id}/transactions/{transaction_id}")]
        public IHttpActionResult PatchTransactions(string entity_id, int transaction_id, [FromBody] Transaction transaction)
        {
            DateTime responseTimeStart = DateTime.Now;
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            try
            {
                if (transaction == null)
                {
                    GeneralLogsController.PostGeneralLog("Transaction", "N/A", "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchTransactions", "Invalid input", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Invalid inputs");
                }
                Entity entity = handlerXML.GetEntity(entity_id);
                RestClient client = new RestClient(entity.Endpoint + "/api");

                RestRequest request = new RestRequest($"transactions/{transaction_id}", Method.PATCH, DataFormat.Json);
                request.AddJsonBody(transaction);
                string auth = Request.Headers.Authorization == null ? "" : Request.Headers.Authorization.ToString();
                request.AddHeader("Authorization", auth);
                IRestResponse<Transaction> response = client.Execute<Transaction>(request);
                //if (response.IsSuccessful && response.Data != null)
                //{
                //    return Content(response.StatusCode, response.Data);
                //}
                GeneralLogsController.PostGeneralLog("Entities", "N/A", "Gateway", response.StatusCode.ToString(), "PatchTransactions", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                dynamic dataDefaultTransaction = JsonConvert.DeserializeObject(response.Content);
                return Content(response.StatusCode, dataDefaultTransaction);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

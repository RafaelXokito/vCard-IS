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
            HandlerXML handlerXML = new HandlerXML(entitiesPath);

            Entity entityCredit = handlerXML.GetEntityByCode(transaction.Payment_Type);

            if (entityCredit == null)
            {
                return NotFound();
            }

            //TODO Get Entity By Endpoint
            Entity entityDebit = handlerXML.GetEntityByCode("VCARD");

            if (entityDebit == null)
            {
                return NotFound();
            }

            RestClient clientDebit = new RestClient(Request.Headers.Referrer.AbsoluteUri.Replace(Request.Headers.Referrer.LocalPath, "") + "/api");
            RestClient clientCredit = new RestClient(entityCredit.Endpoint + "/api");

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

            if (responseDebit.IsSuccessful)
            {
                dynamic dataTransactionDebit = JsonConvert.DeserializeObject(responseDebit.Content);
                if (dataTransactionDebit.data != null)
                    dataTransactionDebit = dataTransactionDebit.data;
                Transaction transactionDebit = new Transaction {
                    Id = dataTransactionDebit.id,
                    Vcard = dataTransactionDebit.vcard,
                    Date = dataTransactionDebit.date,
                    DateTime = Convert.ToDateTime(dataTransactionDebit.datetime.ToString()),
                    Type = dataTransactionDebit.type,
                    Value = dataTransactionDebit.value,
                    Old_Balance = dataTransactionDebit.old_balance,
                    New_Balance = dataTransactionDebit.new_balance,
                    Payment_Type = dataTransactionDebit.payment_type,
                    Payment_Reference = dataTransactionDebit.payment_reference,
                    Category = dataTransactionDebit.category,
                    Description = dataTransactionDebit.description
                };

                TransactionLog transactionLog = new TransactionLog {
                    FromUser = transactionDebit.Vcard,
                    FromEntity = clientDebit.Name,
                    ToUser = transactionDebit.Payment_Reference,
                    ToEntity = transactionDebit.Payment_Type,
                    Amount = transactionDebit.Value,
                    Status = "completed",
                    Message = transactionDebit.Type.ToString(),
                    Timestamp = DateTime.Now,
                };
                TransactionLogsController.PostTransactionLog(transactionLog);

                if (Request.Headers.Referrer.AbsoluteUri.Replace(Request.Headers.Referrer.LocalPath,"") != handlerXML.GetEntityByCode(transactionDebit.Payment_Type).Endpoint)
                {
                    RestRequest requestCredit = new RestRequest("/transactions", Method.POST, DataFormat.Json);
                    requestCredit.AddJsonBody(new
                    {
                        phonenumber = transactionDebit.Payment_Reference.ToString(),
                        paymentreference = transactionDebit.Vcard,
                        value = transaction.Value,
                        paymenttype = "VCARD",
                        type = "C",
                    });
                    requestCredit.AddHeader("Authorization", entityCredit.Authentication.Token);
                    IRestResponse<Transaction> responseCredit = clientCredit.Execute<Transaction>(requestCredit);
                    if (responseCredit.IsSuccessful)
                    {
                        transactionLog = new TransactionLog
                        {
                            FromUser = transactionDebit.Payment_Reference,
                            FromEntity = transactionDebit.Payment_Type,
                            ToUser = transactionDebit.Payment_Reference,
                            ToEntity = "VCARD",
                            Status = "completed",
                            Amount = transactionDebit.Value,
                            Message = "C",
                            Timestamp = DateTime.Now,
                        };
                        TransactionLogsController.PostTransactionLog(transactionLog);
                    }
                    else
                    {
                        //SE A TRANSAÇÃO FALHAR TEMOS DE DEVOLVER O DINHEIRO AO COSTUMER QUE EFETUOU A TRANSFERÊNCIA
                        //COM AS CREDENCIAIS DO GATEWAY 
                        RestRequest requestCreditBack = new RestRequest("/transactions", Method.POST, DataFormat.Json);
                        requestCreditBack.AddJsonBody(new
                        {
                            vcard = transactionDebit.Vcard.ToString(),
                            payment_reference = entityDebit.Authentication.Username,
                            value = transactionDebit.Value,
                            payment_type = "MBWAY",
                            type = "C",
                        });
                        requestCreditBack.AddHeader("Authorization", entityDebit.Authentication.Token);
                        responseCredit = clientDebit.Execute<Transaction>(requestCreditBack);
                        //dynamic dataTransactionCreditBack = JsonConvert.DeserializeObject(responseDebit.Content);
                        //if (dataTransactionCreditBack.data != null)
                        //    dataTransactionCreditBack = dataTransactionCreditBack.data;
                        if (responseCredit.IsSuccessful)
                        {
                            return InternalServerError(new Exception(responseCredit.StatusCode.ToString()));
                        }
                    }
                }
                return Ok(JsonConvert.DeserializeObject(responseDebit.Content));
            }
            return InternalServerError(new Exception(responseDebit.StatusCode.ToString()));
        }
    }
}

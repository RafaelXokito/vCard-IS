using vCardAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace vCardAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        /// <summary>
        /// Search for a Transaction based on given ID based on User authenticated
        /// </summary>
        /// <param name="id">Transaction ID</param>
        /// <returns>Transaction founded</returns>
        /// <response code="200">Returns the Transaction founded</response>
        /// <response code="401">Transaction does not belongs to authenticated user</response>
        /// <response code="404">If the Transaction was not founded</response>
        [BasicAuthentication]
        [Route("api/transactions/{id:int}")]
        public IHttpActionResult GetTransactionAPI(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            Transaction policy = GetTransaction(id);

            if (policy != null && policy.PhoneNumber == phoneNumber)
            {
                string queryString = "SELECT * FROM Transactions WHERE Id = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Transaction transaction = new Transaction()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                PhoneNumber = (string)reader["PhoneNumber"],
                                Date = (DateTime)reader["Date"],
                                Type = (string)reader["Type"],
                                Value = (decimal)reader["Value"],
                                Old_Balance = (decimal)reader["OldBalance"],
                                New_Balance = (decimal)reader["NewBalance"],
                                Payment_Type = (string)reader["PaymentType"],
                                Payment_Reference = (string)reader["PaymentReference"],
                                Category = Convert.ToInt32(reader["ClassificationId"] == DBNull.Value ? 0 : reader["ClassificationId"]),
                                Description = reader["Description"].ToString()
                            };

                            if (transaction.PhoneNumber != phoneNumber)
                            {
                                return Content(HttpStatusCode.Unauthorized, $"Transaction {id} does not belongs to you.");
                            }
                            return Ok(transaction);
                        }

                        reader.Close();
                    }
                    catch (Exception)
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                    return Content(HttpStatusCode.NotFound, $"Transaction {id} does not exist.");
                }

            }
            else
            {
                if (policy != null)
                    return Content(HttpStatusCode.NotFound, $"Transaction {id} does not exist.");

                return Content(HttpStatusCode.Unauthorized, $"Transaction {id} does not belongs to you.");
            }
        }

        private Transaction GetTransaction(int id)
        {
            string queryString = "SELECT * FROM Transactions WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Transaction transaction = new Transaction()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Date = (DateTime)reader["Date"],
                            Type = (string)reader["Type"],
                            Value = (decimal)reader["Value"],
                            Old_Balance = (decimal)reader["OldBalance"],
                            New_Balance = (decimal)reader["NewBalance"],
                            Payment_Type = (string)reader["PaymentType"],
                            Payment_Reference = (string)reader["PaymentReference"],
                            Category = Convert.ToInt32(reader["ClassificationId"] == DBNull.Value ? 0 : reader["ClassificationId"]),
                            Description = reader["Description"].ToString()
                        };

                        return transaction;
                    }

                    reader.Close();
                }
                catch (Exception)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Search for all Transactions based on User authenticated
        /// </summary>
        /// <returns>A list of all Transactions</returns>
        /// <response code="200">Returns the Transactions founded. Returns null if you are not authorized</response>
        [BasicAuthentication]
        [Route("api/transactions")]
        public IEnumerable<Transaction> GetTransactions()
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryString = "SELECT * FROM Transactions WHERE PhoneNumber = @number";

            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@number", phoneNumber);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Date = (DateTime)reader["Date"],
                            Type = (string)reader["Type"],
                            Value = (decimal)reader["Value"],
                            Old_Balance = (decimal)reader["OldBalance"],
                            New_Balance = (decimal)reader["NewBalance"],
                            Payment_Type = (string)reader["PaymentType"],
                            Payment_Reference = (string)reader["PaymentReference"],
                            Category = Convert.ToInt32(reader["ClassificationId"] == DBNull.Value ? 0 : reader["ClassificationId"]),
                            Description = reader["Description"].ToString()
                        };

                        transactions.Add(transaction);
                    }
                    reader.Close();

                    connection.Close();

                }
                catch (Exception)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                return transactions;
            }
        }

        /// <summary>
        /// Insert Transaction from Gateway to User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "Payment_reference": "913406043",
        ///         "Value": "3",
        ///         "Payment_type": "VCARD",
        ///         "Category": "7",
        ///         "Description": "7",
        ///         "Type": "D",
        ///         "PhoneNumber": "900000001" 
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="transaction">Transaction to insert</param>
        /// <returns>Transaction inserted</returns>
        /// <response code="200">Returns the newly created Transaction</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/transactions")]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            //É SUPOSTO APENAS O GATEWAY FAZER TRANSAÇÕES NAS ENTIDADES?
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            //If 'phoneNumber' == "GATEWAY" então espera-se uma transação de crédito
            //If phoneNumber == transaction.PhoneNumber então espera-se uma transação de débito
            //Else Unautherised
            if (phoneNumber == transaction.PhoneNumber || phoneNumber == "GATEWAY")
            {
                string queryStringUser = "SELECT * FROM Users WHERE PhoneNumber = @phonenumber";
                string queryStringTransaction = "INSERT INTO Transactions(PhoneNumber, Type, OldBalance, NewBalance, Value, PaymentType, PaymentReference, ClassificationId, Description) VALUES(@phonenumber, @type, @oldbalance, @newbalance, @value, @paymenttype, @paymentreference, @classificationid, @description);SELECT SCOPE_IDENTITY();";
                string queryStringNewBalance = "UPDATE Users SET Balance = @balance WHERE PhoneNumber = @phonenumber";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {   
                        #region Check User Existence
                        SqlCommand command = new SqlCommand(queryStringUser, connection);

                        command.Parameters.AddWithValue("@phonenumber", transaction.PhoneNumber);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (!reader.Read())
                        {
                            return Content(HttpStatusCode.NotFound, $"User {transaction.PhoneNumber} does not exist."); ;
                        }
                        #endregion

                        #region Validations of User Balance/Maxlimit Level
                        User user = new User()
                        {
                            MaximumLimit = (decimal)reader["MaximumLimit"],
                            Balance = (decimal)reader["Balance"],
                        };

                        reader.Close();

                        //VERIFY BALANCE AND VALUE LIMITS
                        //Just on Debit Transactions
                        //You can receive whatever the transaction owner want
                        if (transaction.Type == "D" && (transaction.Value > user.MaximumLimit || transaction.Value > user.Balance))
                        {
                            return BadRequest("The value of the transaction is invalid");
                        }

                        //Check for Transfer To Same Number
                        if (transaction.PhoneNumber == transaction.Payment_Reference)
                            return Content(HttpStatusCode.BadRequest, $"You can't transfer to your self");

                        #endregion

                        #region Make Transaction
                        command = new SqlCommand(queryStringTransaction, connection);

                        command.Parameters.AddWithValue("@phonenumber", transaction.PhoneNumber);
                        command.Parameters.AddWithValue("@type", transaction.Type[0]);
                        command.Parameters.AddWithValue("@oldbalance", user.Balance);
                        command.Parameters.AddWithValue("@newbalance", (transaction.Type == "C" ? 1 : -1) * transaction.Value + user.Balance);
                        command.Parameters.AddWithValue("@value", transaction.Value);

                        //Debit Transaction Inclusion
                        if (transaction.Type == "D")
                        {
                            #region Verify Category Owner
                            CategoriesController categoriesController = new CategoriesController();
                            if (transaction.Category != 0 && categoriesController.GetCategoryById(Convert.ToInt32(transaction.Category)).Owner != transaction.PhoneNumber)
                                return Content(HttpStatusCode.Unauthorized, $"That category don't belongs to you");
                            #endregion
                            if (transaction.Category != 0)
                                command.Parameters.AddWithValue("@classificationid", transaction.Category);
                            else
                                command.Parameters.AddWithValue("@classificationid", DBNull.Value);

                            command.Parameters.AddWithValue("@description", transaction.Description ?? "");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@classificationid", DBNull.Value);
                            command.Parameters.AddWithValue("@description", "");
                        }
                        command.Parameters.AddWithValue("@paymenttype", transaction.Payment_Type);
                        command.Parameters.AddWithValue("@paymentreference", transaction.Payment_Reference);

                        string insertedID = command.ExecuteScalar().ToString();
                        if (insertedID == null)
                        {
                            return BadRequest("Transaction inputs are invalid");
                        }
                        #endregion

                        #region Update User Balance
                        command = new SqlCommand(queryStringNewBalance, connection);

                        command.Parameters.AddWithValue("@phonenumber", transaction.PhoneNumber);
                        command.Parameters.AddWithValue("@balance", (transaction.Type == "C" ? 1 : -1) * transaction.Value + user.Balance);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            return Ok(GetTransaction(Convert.ToInt32(insertedID)));
                        }
                    
                        connection.Close();
                        return Content(HttpStatusCode.InternalServerError, $"Error updating user {transaction.PhoneNumber} balance.");
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return InternalServerError(ex);
                    }
                }
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, $"You cant make this transaction.");
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
        ///         "Category": "7",
        ///         "Description": "Food",
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Transaction ID</param>
        /// <param name="transaction">Transaction to be updated</param>
        /// <returns>Transaction Updated</returns>
        /// <response code="200">Returns the updated created Transaction</response>
        /// <response code="401">Transaction does not belongs to authenticated user</response>
        /// <response code="404">If given Transaction not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/transactions/{id:int}")]
        public IHttpActionResult PatchTransaction(int id, [FromBody] Transaction transaction)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            Transaction policy = GetTransaction(id);

            if (policy != null && policy.PhoneNumber == phoneNumber)
            {
                string queryString = "UPDATE T SET T.ClassificationId = @classificationid, T.Description = @description FROM Transactions T WHERE T.Id = @id AND T.PhoneNumber = @number";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@id", id);

                    #region Verify Category Owner
                    CategoriesController categoriesController = new CategoriesController();
                    if (transaction.Category != 0 && categoriesController.GetCategoryById(Convert.ToInt32(transaction.Category), phoneNumber).Owner != phoneNumber)
                        return Content(HttpStatusCode.Unauthorized, $"That category don't belongs to you");
                    #endregion

                    if (transaction.Category != 0)
                        command.Parameters.AddWithValue("@classificationid", transaction.Category);
                    else
                        command.Parameters.AddWithValue("@classificationid", DBNull.Value);

                    command.Parameters.AddWithValue("@description", transaction.Description ?? "");
                    command.Parameters.AddWithValue("@number", phoneNumber);

                    try
                    {
                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return Ok(GetTransaction(id));
                        }
                        connection.Close();

                        return Content(HttpStatusCode.BadRequest, $"Something went wrong with your inputs.");
                    }
                    catch (Exception ex)
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return InternalServerError(ex);
                    }
                }

            }
            else
            {
                if (policy != null)
                    return Content(HttpStatusCode.NotFound, $"Transaction {id} does not exist.");

                return Content(HttpStatusCode.Unauthorized, $"Transaction {id} does not belongs to you.");
            }
        }
    }
}

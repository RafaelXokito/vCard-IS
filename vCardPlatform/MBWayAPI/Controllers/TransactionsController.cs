using MBWayAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MBWayAPI.Controllers
{
    public class TransactionsController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        [BasicAuthentication]
        [Route("api/transactions/{id:int}")]
        public IHttpActionResult GetTransactionAPI(int id)
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
                            OldBalance = (decimal)reader["OldBalance"],
                            NewBalance = (decimal)reader["NewBalance"],
                            PaymentType = (string)reader["PaymentType"],
                            PaymentReference = (string)reader["PaymentReference"],
                            ClassificationId = reader["ClassificationId"].ToString(),
                            Description = reader["Description"].ToString()
                        };

                        string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

                        if (transaction.PhoneNumber != phoneNumber)
                        {
                            return Unauthorized();
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
                return NotFound();
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
                            OldBalance = (decimal)reader["OldBalance"],
                            NewBalance = (decimal)reader["NewBalance"],
                            PaymentType = (string)reader["PaymentType"],
                            PaymentReference = (string)reader["PaymentReference"],
                            ClassificationId = reader["ClassificationId"].ToString(),
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
                            OldBalance = (decimal)reader["OldBalance"],
                            NewBalance = (decimal)reader["NewBalance"],
                            PaymentType = (string)reader["PaymentType"],
                            PaymentReference = (string)reader["PaymentReference"],
                            ClassificationId = reader["ClassificationId"].ToString(),
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

        //[BasicAuthentication]
        [Route("api/transactions")]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization) ?? transaction.PhoneNumber;

            string queryStringUser = "SELECT * FROM Users WHERE PhoneNumber = @phonenumber";
            string queryStringTransaction = "INSERT INTO Transactions(PhoneNumber, Type, OldBalance, NewBalance, Value, PaymentType, PaymentReference) VALUES(@phonenumber, @type, @oldbalance, @newbalance, @value, @paymenttype, @paymentreference);SELECT SCOPE_IDENTITY();";
            string queryStringNewBalance = "UPDATE Users SET Balance = @balance WHERE PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringUser, connection);

                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.Read())
                    {
                        return NotFound();
                    }

                    User user = new User()
                    {
                        MaximumLimit = (decimal)reader["MaximumLimit"],
                        Balance = (decimal)reader["Balance"],
                    };

                    reader.Close();

                    //VERIFY BALANCE AND VALUE LIMITS
                    if (transaction.Value > user.MaximumLimit || transaction.Value > user.Balance)
                    {
                        return BadRequest("The value of the transaction is invalid");
                    }

                    command = new SqlCommand(queryStringTransaction, connection);

                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);
                    command.Parameters.AddWithValue("@type", transaction.Type[0]);
                    command.Parameters.AddWithValue("@oldbalance", user.Balance);
                    command.Parameters.AddWithValue("@newbalance", (transaction.Type == "C" ? 1 : -1) * transaction.Value + user.Balance);
                    command.Parameters.AddWithValue("@value", transaction.Value);
                    command.Parameters.AddWithValue("@paymenttype", transaction.PaymentType);
                    command.Parameters.AddWithValue("@paymentreference", transaction.PaymentReference);

                    string insertedID = command.ExecuteScalar().ToString();
                    if (insertedID == null)
                    {
                        return BadRequest();
                    }

                    command = new SqlCommand(queryStringNewBalance, connection);

                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);
                    command.Parameters.AddWithValue("@balance", (transaction.Type == "C" ? 1 : -1) * transaction.Value + user.Balance);

                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok(GetTransaction(Convert.ToInt32(insertedID)));
                    }
                    
                    connection.Close();
                    return BadRequest();

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

        [BasicAuthentication]
        [Route("api/transactions/{id:int}")]
        public IHttpActionResult PatchTransaction(int id, [FromBody] Transaction transaction)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryString = "UPDATE T SET T.ClassificationId = @classificationid, T.Description = @description FROM Transactions T WHERE T.Id = @id AND T.PhoneNumber = @number AND (T.ClassificationId IS NULL OR T.ClassificationId IN (SELECT C.Id FROM Categories C WHERE C.Owner = T.PhoneNumber AND C.Type = T.Type))";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@classificationid", transaction.ClassificationId);
                command.Parameters.AddWithValue("@description", transaction.Description);
                command.Parameters.AddWithValue("@number", phoneNumber);

                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok();
                    }
                    connection.Close();

                    return NotFound();
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
    }
}

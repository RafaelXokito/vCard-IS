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

        [Route("api/transactions/{id:int}")]
        public IHttpActionResult GetTransaction(int id)
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
                            ClassificationId = (int)reader["ClassificationId"]
                        };
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

        [Route("api/transactions")]
        public IEnumerable<Transaction> GetTransactions()
        {
            string queryString = "SELECT * FROM Transactions";

            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
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
                            ClassificationId = (int)reader["ClassificationId"]
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

        [Route("api/users/{number:int}/transactions/{id:int}")]
        public IHttpActionResult GetUserTransaction(int number, int id)
        {
            string queryString = "SELECT * FROM Transactions WHERE Id = @id AND PhoneNumber = @number";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@number", number);
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
                            ClassificationId = (int)reader["ClassificationId"]
                        };
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

        [Route("api/users/{number:int}/transactions")]
        public IEnumerable<Transaction> GetUserTransactions(int number)
        {
            string queryString = "SELECT * FROM Transactions WHERE PhoneNumber = @number";

            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@number", number);

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
                            ClassificationId = (int)reader["ClassificationId"]
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

        [Route("api/users/{number:int}/transactions")]
        public IHttpActionResult PostUserTransaction(int number, [FromBody] Transaction transaction)
        {
            string queryStringUser = "SELECT * FROM Users WHERE PhoneNumber = @number";
            string queryString = "INSERT INTO Transactions(PhoneNumber, Type, Value, PaymentType, PaymentReference) VALUES(@phonenumber, @type, @value, @paymenttype, @paymentreference)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringUser, connection);

                    command.Parameters.AddWithValue("@number", number);

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

                    if (transaction.Value > user.MaximumLimit || transaction.Value > user.Balance)
                    {
                        throw new Exception("The value of the transaction is invalid");
                    }

                    command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", number);
                    command.Parameters.AddWithValue("@type", transaction.Type);
                    command.Parameters.AddWithValue("@value", transaction.Value);
                    command.Parameters.AddWithValue("@paymenttype", transaction.PaymentType);
                    command.Parameters.AddWithValue("@paymentreference", transaction.PaymentReference);

                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok();
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

        [Route("api/users/{number:int}/transactions/{id:int}")]
        public IHttpActionResult PatchTransactionClassification(int number, int id, [FromBody] Transaction transaction)
        {
            string queryString = "UPDATE DefaultCategories SET ClassificationId = @classificationid WHERE Id = @id AND @classificationid IN (SELECT Id FROM Categories WHERE Owner = @owner)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@classificationid", transaction.ClassificationId);
                command.Parameters.AddWithValue("@owner", number);

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

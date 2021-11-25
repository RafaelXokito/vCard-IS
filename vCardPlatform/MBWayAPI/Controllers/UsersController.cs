using MBWayAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace MBWayAPI.Controllers
{
    public class UsersController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        [Route("api/users/{number:int}")]
        public IHttpActionResult GetUser(int number)
        {
            string queryString = "SELECT * FROM Users WHERE PhoneNumber = @number";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@number", number);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        User product = new User()
                        {
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            MaximumLimit = (decimal)reader["MaximumLimit"],
                            Balance = (decimal)reader["Balance"],
                            Photo = reader["Photo"] == null ? "" : (string)reader["Photo"]
                        };
                        return Ok(product);
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

        [Route("api/users")]
        public IEnumerable<User> GetUsers()
        {
            string queryString = "SELECT * FROM Users";

            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            MaximumLimit = (decimal)reader["MaximumLimit"],
                            Balance = (decimal)reader["Balance"],
                            Photo = reader["Photo"] == null ? "" : (string)reader["Photo"],
                        };

                        users.Add(user);
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
                return users;
            }
        }

        [Route("api/users")]
        public IHttpActionResult PostUser(User user)
        {
            string queryString = "INSERT INTO Users(PhoneNumber, Password, Name, Email, ConfirmationCode, MaximumLimit, Photo) VALUES(@phonenumber, @password, @name, @email, @confirmationcode, @maximumlimit, @photo)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);

                    decimal maxLimit = user.MaximumLimit == 0 ? 5000 : user.MaximumLimit;
                    command.Parameters.AddWithValue("@maximumlimit", maxLimit);

                    string photo = user.Photo == null ? "" : user.Photo;
                    command.Parameters.AddWithValue("@photo", photo);

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        string passwordHash = GetHash(sha256, user.Password);
                        command.Parameters.AddWithValue("@password", passwordHash);

                        string confirmationCodeHash = GetHash(sha256, user.ConfirmationCode);
                        command.Parameters.AddWithValue("@confirmationcode", confirmationCodeHash);
                    }

                    connection.Open();

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

        [Route("api/users/{number:int}")]
        public IHttpActionResult PutUser(int number, [FromBody] User user)
        {
            string queryString = "UPDATE Users SET Password = @password, Name = @name, Email = @email, ConfirmationCode = @confirmationcode, MaximumLimit = @maximumlimit, Photo = @photo Where PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@phonenumber", number);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@email", user.Email);

                decimal maxLimit = user.MaximumLimit == 0 ? 5000 : user.MaximumLimit;
                command.Parameters.AddWithValue("@maximumlimit", maxLimit);

                string photo = user.Photo == null ? "" : user.Photo;
                command.Parameters.AddWithValue("@photo", photo);

                using (SHA256 sha256 = SHA256.Create())
                {
                    string passwordHash = GetHash(sha256, user.Password);
                    command.Parameters.AddWithValue("@password", passwordHash);

                    string confirmationCodeHash = GetHash(sha256, user.ConfirmationCode);
                    command.Parameters.AddWithValue("@confirmationcode", confirmationCodeHash);
                }

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

        [Route("api/users/{number:int}")]
        public IHttpActionResult DeleteUser(int number)
        {
            string queryString = "DELETE FROM Users WHERE PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@phonenumber", number);

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

        private string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfInput = GetHash(hashAlgorithm, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}

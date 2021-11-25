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
                            ConfirmationCode = (string)reader["ConfirmationCode"],
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
                            ConfirmationCode = (string)reader["ConfirmationCode"],
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
    }
}

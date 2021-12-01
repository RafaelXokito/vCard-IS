using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class AdministratorsController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        [Route("api/administrators/{email}")]
        public IHttpActionResult GetAdministrator(string email)
        {
            string queryString = "SELECT * FROM Administrators WHERE Email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@email", email);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Administrator administrator = new Administrator()
                        {
                            Email = (string)reader["Email"],
                            Name = (string)reader["Name"],
                            Disabled = (bool)reader["Disabled"]
                        };
                        return Ok(administrator);
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

        [Route("api/administrators")]
        public IEnumerable<Administrator> GetAdministrators()
        {
            string queryString = "SELECT * FROM Administrators";

            List<Administrator> administrators = new List<Administrator>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Administrator user = new Administrator()
                        {
                            Email = (string)reader["Email"],
                            Name = (string)reader["Name"],
                            Disabled = (bool)reader["Disabled"]
                        };

                        administrators.Add(user);
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
                return administrators;
            }
        }

        [Route("api/users")]
        public IHttpActionResult PostAdministrator(Administrator administrator)
        {
            string queryString = "INSERT INTO Administrators(Email, Password, Name) VALUES(@email, @password, @name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@email", administrator.Email);
                    command.Parameters.AddWithValue("@name", administrator.Name);

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        string passwordHash = GetHash(sha256, administrator.Password);
                        command.Parameters.AddWithValue("@password", passwordHash);
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

        [Route("api/administrators/{email}")]
        public IHttpActionResult PutAdministrator(string email, [FromBody] Administrator administrator)
        {
            string queryString = "UPDATE Administrators SET Name = @name, Disabled = @disabled WHERE Email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@name", administrator.Name);
                command.Parameters.AddWithValue("@disabled", administrator.Disabled ? 1 : 0);
                command.Parameters.AddWithValue("@email", email);

                using (SHA256 sha256 = SHA256.Create())
                {
                    string passwordHash = GetHash(sha256, administrator.Password);
                    command.Parameters.AddWithValue("@password", passwordHash);
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

        [Route("api/administrators/{email}/disabled")]
        public IHttpActionResult PatchAdministratorDisabled(string email, [FromBody] Administrator administrator)
        {
            string queryString = "UPDATE Administrators SET Disabled = @disabled WHERE Email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@disabled", administrator.Disabled ? 1 : 0);
                command.Parameters.AddWithValue("@email", email);

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

        public class Secret
        {
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }

        [Route("api/administrators/{email}/password")]
        public IHttpActionResult PatchAdministratorPassword(string email, [FromBody] Secret secret)
        {
            string queryString = "UPDATE Administrators SET Password = @newpassword WHERE Email = @email AND Password = @oldpassword";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@email", email);

                using (SHA256 sha256 = SHA256.Create())
                {
                    string oldPasswordHash = GetHash(sha256, secret.Password);
                    command.Parameters.AddWithValue("@oldpassword", oldPasswordHash);

                    string newPasswordHash = GetHash(sha256, secret.NewPassword);
                    command.Parameters.AddWithValue("@newpassword", newPasswordHash);
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

        [Route("api/administrators/{email}")]
        public IHttpActionResult DeleteUser(string email)
        {
            string queryString = "DELETE FROM Administrators WHERE Email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@email", email);

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
    }
}

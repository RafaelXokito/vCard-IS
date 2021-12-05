using MBWayAPI;
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

        [Route("api/login")]
        public IHttpActionResult PostSignin(Credentials credentials)
        {
            if (AdminValidate.Login(credentials.Email, credentials.Password))
            {
                return Ok();
            }
            return Unauthorized();
        }

        [BasicAuthentication]
        [Route("api/me")]
        public IHttpActionResult GetLoggedAdministrator()
        {
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);

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
                            Id = Convert.ToInt32(reader["Id"]),
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

        [BasicAuthentication]
        [Route("api/administrators/{id:int}")]
        public IHttpActionResult GetAdministrator(int id)
        {
            string queryString = "SELECT * FROM Administrators WHERE Id = @id";

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
                        Administrator administrator = new Administrator()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
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

        [BasicAuthentication]
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
                        Administrator administrator = new Administrator()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Email = (string)reader["Email"],
                            Name = (string)reader["Name"],
                            Disabled = (bool)reader["Disabled"]
                        };

                        administrators.Add(administrator);
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

        [BasicAuthentication]
        [Route("api/administrators")]
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

        [BasicAuthentication]
        [Route("api/administrators/{id:int}")]
        public IHttpActionResult PutAdministrator(int id, [FromBody] Administrator administrator)
        {
            string queryString = "UPDATE Administrators SET Name = @name, Disabled = @disabled WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@name", administrator.Name);
                command.Parameters.AddWithValue("@disabled", administrator.Disabled ? 1 : 0);
                command.Parameters.AddWithValue("@id", id);

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

        [BasicAuthentication]
        [Route("api/administrators/{id:int}/disabled")]
        public IHttpActionResult PatchAdministratorDisabled(int id, [FromBody] Administrator administrator)
        {
            string queryString = "UPDATE Administrators SET Disabled = @disabled WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@disabled", administrator.Disabled ? 1 : 0);
                command.Parameters.AddWithValue("@id", id);

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

        [BasicAuthentication]
        [Route("api/administrators/{id:int}/password")]
        public IHttpActionResult PatchAdministratorPassword(int id, [FromBody] Secret secret)
        {
            string queryString = "UPDATE Administrators SET Password = @newpassword WHERE Id = @id AND Password = @oldpassword";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);

                using (SHA256 sha256 = SHA256.Create())
                {
                    //string oldPasswordHash = GetHash(sha256, secret.Password);
                    string oldPasswordHash = secret.Password;
                    command.Parameters.AddWithValue("@oldpassword", oldPasswordHash);

                    //string newPasswordHash = GetHash(sha256, secret.NewPassword);
                    string newPasswordHash = secret.NewPassword;
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

        [BasicAuthentication]
        [Route("api/administrators/{id:int}")]
        public IHttpActionResult DeleteAdministrator(int id)
        {
            string queryString = "DELETE FROM Administrators WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);

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
            if (input == null) return null;

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

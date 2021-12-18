using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class AdministratorsController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        /// <summary>
        /// Try to signin with gateway administrator credentials
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Email": "a1@mail.pt",
        ///        "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="credentials">Admin Credentials</param>
        /// <returns>Subjetive message</returns>
        /// <response code="200">If logged in successfuly</response>
        /// <response code="400">Admin credentials are wrong</response>
        [Route("api/login")]
        public IHttpActionResult PostSignin(Credentials credentials)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (AdminValidate.Login(credentials.Email, credentials.Password))
            {
                stopwatch.Stop();
                GeneralLogsController.PostGeneralLog("Authentication", credentials.Email, "Gateway", HttpStatusCode.OK.ToString(), $"Authenticate with {credentials.Email}", "", DateTime.Now, stopwatch.ElapsedMilliseconds);
                return Ok(credentials.Email + " Success");
            }
            stopwatch.Stop();
            GeneralLogsController.PostGeneralLog("Authentication", credentials.Email, "Gateway", HttpStatusCode.BadRequest.ToString(), $"Failed to Authenticate with {credentials.Email}", "Invalid Email and/or Password", DateTime.Now, stopwatch.ElapsedMilliseconds);
            return BadRequest("Invalid Email and/or Password");
        }

        /// <summary>
        /// Search current authenticated gateway administrator
        /// </summary>
        /// <returns>Autenticated user</returns>
        /// <response code="200">Returns the User found</response>
        /// <response code="404">If we could not identify user</response>
        [BasicAuthentication]
        [Route("api/me")]
        public IHttpActionResult GetLoggedAdministrator()
        {
            DateTime responseTimeStart = DateTime.Now;
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
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "GetLoggedAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));

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
                GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.NotFound.ToString(), "GetLoggedAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));

                return Content(HttpStatusCode.NotFound, "We could not identify you");
            }
        }

        /// <summary>
        /// Search a gateway administrator
        /// </summary>
        /// <returns>Autenticated user</returns>
        /// <response code="200">Returns the User found</response>
        /// <response code="404">If we could not identify user</response>
        [BasicAuthentication]
        [Route("api/administrators/{id:int}")]
        public IHttpActionResult GetAdministrator(int id)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);

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
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "GetAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
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
                GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.NotFound.ToString(), "GetAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return Content(HttpStatusCode.NotFound, "Administrator not found");
            }
        }

        /// <summary>
        /// Search for all gateway administrators
        /// </summary>
        /// <returns>A list of all gateway administrators</returns>
        /// <response code="200">Returns the users found. Returns null if you are not authorized</response>
        [BasicAuthentication]
        [Route("api/administrators")]
        public IEnumerable<Administrator> GetAdministrators()
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
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
                GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "GetAdministrators", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                return administrators;
            }
        }

        /// <summary>
        /// Create gateway Administrator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///       "Name": "Jhon",
        ///       "Email": "900000001@mail.pt",
        ///       "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="administrator">gateway Administrator to insert</param>
        /// <returns>gateway Administrator inserted</returns>
        /// <response code="201">If administrator was created</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/administrators")]
        public IHttpActionResult PostAdministrator(Administrator administrator)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            string queryString = "INSERT INTO Administrators(Email, Password, Name) VALUES(@email, @password, @name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    if (administrator.Name == null || administrator.Name == "")
                    {
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PostAdministrator", "Name is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.BadRequest, "Administrator Name cant be null or empty");
                    }
                    if (administrator.Email == null || administrator.Email == "")
                    {
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PostAdministrator", "Email is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.BadRequest, "Administrator Email cant be null or empty");
                    }
                    if (administrator.Password == null || administrator.Password == "")
                    {
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PostAdministrator", "Password is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.BadRequest, "Administrator Password cant be null or empty");
                    }
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
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.Created.ToString(), "PostAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.Created, $"Administrator {administrator.Name} created");
                    }

                    connection.Close();
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PostAdministrator", "Something went wrong with your inputs.", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Something went wrong with your inputs.");
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PostAdministrator", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return InternalServerError(ex);
                }
            }
        }

        /// <summary>
        /// Update gateway Administrator
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///       "Name": "Jhon"
        ///     }
        ///     
        ///     Disabled in (0, 1)
        ///     
        /// </remarks>
        /// <param name="id">Admin ID</param>
        /// <param name="administrator">gateway Administrator body to update</param>
        /// <returns>gateway Administrator updated</returns>
        /// <response code="200">If administrator was updated</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/administrators/{id:int}")]
        public IHttpActionResult PutAdministrator(int id, [FromBody] Administrator administrator)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            string queryString = "UPDATE Administrators SET Name = @name WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                if (administrator == null || (administrator.Name == null || administrator.Name == ""))
                {
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PutAdministrator", "Name is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Administrator name cant be null or empty");
                }

                command.Parameters.AddWithValue("@name", administrator.Name);
                //command.Parameters.AddWithValue("@disabled", administrator.Disabled ? 1 : 0);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "PutAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.OK, $"Administrator {administrator.Name} updated");
                    }
                    connection.Close();
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PutAdministrator", "Something went wrong with inputs", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Something went wrong with inputs");
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PutAdministrator", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return InternalServerError(ex);
                }
            }
        }

        /// <summary>
        /// Disable or Enable gateway Administrator
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH
        ///     {
        ///         "Disabled": 1
        ///     }
        ///     
        ///     Disabled in (0, 1)
        ///
        /// </remarks>
        /// <param name="id">Admin ID</param>
        /// <param name="administrator">Administrator struct body used to disable or enable</param>
        /// <returns>Gateway Administrator Updated</returns>
        /// <response code="200">If given gateway Administrator was disabled/enabled</response>
        /// <response code="404">If given gateway Administrator not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/administrators/{id:int}/disabled")]
        public IHttpActionResult PatchAdministratorDisabled(int id, [FromBody] Administrator administrator)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            string queryString = "UPDATE Administrators SET Disabled = @disabled WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (administrator == null)
                {
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchAdministratorDisabled", "Disabled is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Administrator Disabled cant be null or empty");
                }
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@disabled", administrator.Disabled ? 1 : 0);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "PatchAdministratorDisabled", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.OK, $"Administrator {administrator.Name} " + (administrator.Disabled ? "disabled" : "enabled"));
                    }
                    connection.Close();

                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.NotFound.ToString(), "PatchAdministratorDisabled", "Administrator was not found", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.NotFound, "Administrator was not found");
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.NotFound.ToString(), "PatchAdministratorDisabled", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return InternalServerError(ex);
                }
            }
        }

        /// <summary>
        /// Update gateway Administrator Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH
        ///     {
        ///         "Password": "1234",
        ///         "NewPassword": "1234",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Admin ID</param>
        /// <param name="secret">Secret struct body used to update</param>
        /// <returns>Gateway Administrator Updated</returns>
        /// <response code="200">Returns the updated gateway Administrator</response>
        /// <response code="404">If given gateway Administrator not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/administrators/{id:int}/password")]
        public IHttpActionResult PatchAdministratorPassword(int id, [FromBody] Secret secret)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
            string queryString = "UPDATE Administrators SET Password = @newpassword WHERE Id = @id AND Password = @oldpassword";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (secret == null || secret.Password == null || secret.Password == "")
                {
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchAdministratorPassword", "Password is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Administrator Password cant be null or empty");
                }
                if (secret == null || secret.NewPassword == null || secret.NewPassword == "")
                {
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.BadRequest.ToString(), "PatchAdministratorPassword", "NewPassword is required", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.BadRequest, "Administrator NewPassword cant be null or empty");
                }
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);

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
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "PatchAdministratorPassword", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.OK, $"Administrator {id} updated");
                    }
                    connection.Close();

                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.NotFound.ToString(), "PatchAdministratorPassword", "Administrator was not found", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.NotFound, "Administrator was not found");
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "PatchAdministratorPassword", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return InternalServerError(ex);
                }
            }
        }

        /// <summary>
        /// Delete a gateway administrator based on given ID
        /// </summary>
        /// <param name="id">Admin ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="404">If given admin not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/administrators/{id:int}")]
        public IHttpActionResult DeleteAdministrator(int id)
        {
            DateTime responseTimeStart = DateTime.Now;
            string email = AdminValidate.GetAdministratorEmailAuth(Request.Headers.Authorization);
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
                        GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.OK.ToString(), "DeleteAdministrator", "", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                        return Content(HttpStatusCode.OK, $"Administrator {id} deleted");
                    }
                    connection.Close();

                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.NotFound.ToString(), "DeleteAdministrator", "Administrator was not found", DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
                    return Content(HttpStatusCode.NotFound, "Administrator was not found");
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    GeneralLogsController.PostGeneralLog("Administrator", email, "Gateway", HttpStatusCode.InternalServerError.ToString(), "DeleteAdministrator", ex.Message, DateTime.Now, Convert.ToInt64((DateTime.Now - responseTimeStart).TotalMilliseconds));
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

using vCardAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace vCardAPI.Controllers
{
    /// <summary>
    /// User rest api
    /// </summary>
    public class UsersController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        /// <summary>
        /// Try to signin with user credentials
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Username": "900000001",
        ///        "Password": "1234"
        ///     }
        ///     
        /// </remarks>
        /// <param name="credentials">User Credentials</param>
        /// <returns>User auth token</returns>
        /// <response code="200">User auth token generated</response>
        /// <response code="400">User credentials are wrong</response>
        [Route("api/signin")]
        public IHttpActionResult PostSignin([FromBody]Credentials credentials)
        {
            if (UserValidate.Login(credentials.Username, credentials.Password)) { 
                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(credentials.Username + ":" + credentials.Password));
                return Ok(new { user=new { token_type = "Basic", access_token = encoded} });
            }
            return Content(HttpStatusCode.BadRequest, $"Invalid Credentials");
        }

        /// <summary>
        /// Search for a user based on given number
        /// </summary>
        /// <param name="number">User Phone Number</param>
        /// <returns>User found</returns>
        /// <response code="200">Returns the User found</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If the User was not found</response>
        [BasicAuthentication]
        [Route("api/users/{number:int}")]
        public IHttpActionResult GetUsers(int number)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryString = "SELECT * FROM Users WHERE PhoneNumber = @number";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@number", number.ToString());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        User user = new User()
                        {
                            Username = (string)reader["PhoneNumber"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            MaximumLimit = (decimal)reader["MaximumLimit"],
                            Balance = (decimal)reader["Balance"],
                            Photo = reader["Photo"].ToString()
                        };
                        return Ok(user);
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
                return Content(HttpStatusCode.NotFound, $"User {phoneNumber} was not found");
            }
        }

        public User GetUserByNumber(string number)
        {
            string queryString = "SELECT * FROM Users WHERE PhoneNumber = @number";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@number", number);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        User user = new User()
                        {
                            Username = (string)reader["PhoneNumber"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            MaximumLimit = (decimal)reader["MaximumLimit"],
                            Balance = (decimal)reader["Balance"],
                            Photo = reader["Photo"].ToString()
                        };
                        return user;
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
        /// Search for all users based on User authenticated
        /// </summary>
        /// <returns>A list of all users</returns>
        /// <response code="200">Returns the users found. Returns null if you are not authorized</response>
        [BasicAuthentication]
        [Route("api/users")]
        public IEnumerable<User> GetUsers()
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumber == "GATEWAY")
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
                                Username = (string)reader["PhoneNumber"],
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
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Insert User for authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Photo": Base64(Image)
        ///     }
        ///     
        /// </remarks>
        /// <param name="phonenumber">User Phone Number</param>
        /// <param name="input">User with photo param as base64 string</param>
        /// <returns>User updated</returns>
        /// <response code="200">Returns the updated created User</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/users/{phonenumber}/photo")]
        public IHttpActionResult PostUserPhoto(string phonenumber, User input)
        {
            string phoneNumberAuth = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumberAuth != "GATEWAY" && phoneNumberAuth != phonenumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {phonenumber}. Or you are not authorized to change this user photo.");
            }
            string queryString = "UPDATE Users SET Photo = @photo WHERE PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", phonenumber);

                    byte[] imageBytes = Convert.FromBase64String(input.Photo);
                    // Convert byte[] to Image
                    Image image;
                    using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        image = Image.FromStream(ms, true);
                    }

                    string startupPath = AppDomain.CurrentDomain.BaseDirectory + "Resources" + $"\\{phonenumber}.jpg";
                    var i2 = new Bitmap(image);
                    i2.Save(startupPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    command.Parameters.AddWithValue("@photo", $"/Resources/{phonenumber}.jpg");

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok(GetUserByNumber(phonenumber));
                    }
                    connection.Close();

                    return Content(HttpStatusCode.NotFound, $"User {phonenumber} not found.");
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

        /// <summary>
        /// Insert User for authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///       "Username": "900000001",
        ///       "Password": "1234",
        ///       "Name": "Jhon",
        ///       "Email": "900000001@mail.pt",
        ///       "ConfirmationCode": "1234",
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="user">User to insert</param>
        /// <returns>User inserted</returns>
        /// <response code="200">Returns the newly created User</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [Route("api/users")]
        public IHttpActionResult PostUser(User user)
        {
            string queryStringDefaultCategories = "SELECT * FROM DefaultCategories";
            string queryStringCategory = "INSERT INTO Categories(Name, Type, Owner) VALUES(@name, @type, @owner)";
            string queryStringUser = "INSERT INTO Users(PhoneNumber, Password, Name, Email, ConfirmationCode, MaximumLimit, Photo) VALUES(@phonenumber, @password, @name, @email, @confirmationcode, @maximumlimit, @photo)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    if (!IsValidPhone(user.Username))
                        return BadRequest($"Phone Number must match portuguese phone number");
                    #region CREATE USER
                    SqlCommand command = new SqlCommand(queryStringUser, connection);

                    command.Parameters.AddWithValue("@phonenumber", user.Username);
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

                    if (command.ExecuteNonQuery() == 0)
                    {
                        return BadRequest($"Something went wrong with inputs");
                    }
                    #endregion

                    #region GET ALL DEFAULT CATEGORIES
                    command = new SqlCommand(queryStringDefaultCategories, connection);

                    List<DefaultCategory> categories = new List<DefaultCategory>();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DefaultCategory category = new DefaultCategory()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = (string)reader["Name"],
                            Type = (string)reader["Type"]
                        };

                        categories.Add(category);
                    }
                    reader.Close();
                    #endregion

                    #region CREATE ALL CATEGORIES OF USER
                    foreach (DefaultCategory defaultCategory in categories)
                    {
                        command = new SqlCommand(queryStringCategory, connection);

                        command.Parameters.AddWithValue("@name", defaultCategory.Name);
                        command.Parameters.AddWithValue("@type", defaultCategory.Type);
                        command.Parameters.AddWithValue("@owner", user.Username);

                        if (command.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Error creating user categories from default categories");
                        }
                    }

                    reader.Close();
                    connection.Close();
                    #endregion

                    return Ok(GetUserByNumber(user.Username));
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

        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^([9][1236])[0-9]*$");
                return r.IsMatch(Phone);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "Name": "John Doe",
        ///         "Email": "jhondoe@mail.pt",
        ///     }
        ///     
        /// </remarks>
        /// <param name="number">User Phone Number</param>
        /// <param name="user">User to be updated</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/users/{number:int}")]
        public IHttpActionResult PutUser(int number, [FromBody]User user)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryString = "UPDATE Users SET Name = @name, Email = @email WHERE PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", number.ToString());
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok(GetUserByNumber(phoneNumber));
                    }
                    connection.Close();

                    return Content(HttpStatusCode.NotFound, $"User does not exist.");
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


        /// <summary>
        /// Update authenticated User Maximum Limit
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "MaximumLimit": "500"
        ///     }
        ///
        /// </remarks>
        /// <param name="number">User Phone Number</param>
        /// <param name="user">Secret struct to be updated</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/users/{number:int}/maxlimit")]
        public IHttpActionResult PatchUserMaxLimit(int number, [FromBody] User user)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryString = "UPDATE Users SET MaximumLimit = @MaximumLimit WHERE PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", number.ToString());
                    command.Parameters.AddWithValue("@MaximumLimit", user.MaximumLimit);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok(GetUserByNumber(phoneNumber));
                    }
                    connection.Close();

                    return Content(HttpStatusCode.NotFound, $"User does not exist.");
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
            public string NewConfirmationCode { get; set; }
        }

        /// <summary>
        /// Update authenticated User Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "Password": "1234",
        ///         "NewPassword": "1234",
        ///     }
        ///
        /// </remarks>
        /// <param name="number">User Phone Number</param>
        /// <param name="secret">Secret struct to be updated</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/users/{number:int}/password")]
        public IHttpActionResult PatchUserPassword(int number, [FromBody] Secret secret)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryString = "UPDATE Users SET Password = @newpassword WHERE PhoneNumber = @phonenumber AND Password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", number.ToString());

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        string oldPasswordHash = GetHash(sha256, secret.Password);
                        command.Parameters.AddWithValue("@password", oldPasswordHash);

                        string newPasswordHash = GetHash(sha256, secret.NewPassword);
                        command.Parameters.AddWithValue("@newpassword", newPasswordHash);
                    }

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok(GetUserByNumber(phoneNumber));
                    }
                    connection.Close();

                    return Content(HttpStatusCode.NotFound, $"User does not exist.");
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

        /// <summary>
        /// Update authenticated User Confirmation Code
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "Password": "1234",
        ///         "NewConfirmationCode": "1234",
        ///     }
        ///
        /// </remarks>
        /// <param name="number">User Phone Number</param>
        /// <param name="secret">Secret struct to be updated</param>
        /// <returns>User Updated</returns>
        /// <response code="200">Returns the updated updated User</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="404">If given User not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/users/{number:int}/confirmationcode")]
        public IHttpActionResult PatchUserConfirmationCode(int number, [FromBody] Secret secret)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryString = "UPDATE Users SET ConfirmationCode = @newconfirmationcode WHERE PhoneNumber = @phonenumber AND Password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", number.ToString());

                    using (SHA256 sha256 = SHA256.Create())
                    {
                        string passwordHash = GetHash(sha256, secret.Password);
                        command.Parameters.AddWithValue("@password", passwordHash);

                        string newConfirmationCodeHash = GetHash(sha256, secret.NewConfirmationCode);
                        command.Parameters.AddWithValue("@newconfirmationcode", newConfirmationCodeHash);
                    }

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok(GetUserByNumber(phoneNumber));
                    }
                    connection.Close();

                    return Content(HttpStatusCode.NotFound, $"User does not exist.");
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


        /// <summary>
        /// Delete a user based on given number and on User authenticated
        /// </summary>
        /// <param name="number">User Phone Number</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns the subjective message</response>
        /// <response code="400">Something went wrong</response>
        /// <response code="401">User does not belongs to authenticated user</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/users/{number:int}")]
        public IHttpActionResult DeleteUser(int number)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryStringGetUser = "SELECT * FROM Users WHERE PhoneNumber = @phonenumber";
            string queryStringDelUser = "DELETE FROM Users WHERE PhoneNumber = @phonenumber";
            string queryStringDelCategories = "DELETE FROM Categories WHERE Id = @id AND Owner = @owner";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringGetUser, connection);

                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        decimal balance = (decimal)reader["Balance"];

                        if (balance > 0)
                        {
                            return BadRequest("The user must have a balance of zero euros to be deleted");
                        }
                    }

                    reader.Close();

                    command = new SqlCommand(queryStringDelUser, connection);

                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);

                    if (command.ExecuteNonQuery() == 0)
                    {
                        connection.Close();
                        return Content(HttpStatusCode.BadRequest, "Something went wrong.");
                    }

                    command = new SqlCommand(queryStringDelCategories, connection);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

                    if (command.ExecuteNonQuery() > 0)
                    {
                        return Ok("User deleted with success");
                    }

                    connection.Close();
                    return Content(HttpStatusCode.BadRequest, "Something went wrong.");
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

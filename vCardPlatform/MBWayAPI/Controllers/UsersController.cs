using MBWayAPI.Models;
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

namespace MBWayAPI.Controllers
{
    public class UsersController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        [Route("api/signin")]
        public IHttpActionResult PostSignin([FromBody]Credentials credentials)
        {
            if (UserValidate.Login(credentials.Username, credentials.Password)) { 
                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(credentials.Username + ":" + credentials.Password));
                return Ok(new { user=new { token_type = "Basic", access_token = encoded} });
            }
            return Content(HttpStatusCode.BadRequest, $"Invalid Credentials");
        }

        [BasicAuthentication]
        [Route("api/users/{number:int}")]
        public IHttpActionResult GetUser(int number)
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
                return NotFound();
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

        [BasicAuthentication] //You only have to be logged in because there are no admins in this Entity
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
                        return BadRequest();
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

        [BasicAuthentication]
        [Route("api/users/{number:int}")]
        public IHttpActionResult PutUser(int number, [FromBody] User user)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            if (number.ToString() != phoneNumber)
            {
                return Content(HttpStatusCode.Unauthorized, $"You are not the user {number}.");
            }

            string queryString = "UPDATE Users SET Name = @name, Email = @email, Photo = @photo WHERE PhoneNumber = @phonenumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@phonenumber", number);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);

                    string photo = user.Photo == null ? "" : user.Photo;
                    command.Parameters.AddWithValue("@photo", photo);

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
            public string NewConfirmationCode { get; set; }
        }

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

                    command.Parameters.AddWithValue("@phonenumber", number);

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

                    command.Parameters.AddWithValue("@phonenumber", number);

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

                    command = new SqlCommand(queryStringDelUser, connection);

                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);

                    if (command.ExecuteNonQuery() == 0)
                    {
                        connection.Close();
                        return NotFound();
                    }

                    command = new SqlCommand(queryStringDelCategories, connection);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

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

using MBWayAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MBWayAPI.Controllers
{
    public class DefaultCategoriesController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        /// <summary>
        /// Search for a category based on given ID based on User authenticated
        /// </summary>
        /// <param name="id">Default Categoy ID</param>
        /// <returns>Default Categoy founded</returns>
        /// <response code="200">Returns the Default Categoy founded</response>
        /// <response code="401">Default Categoy does not belongs to authenticated user</response>
        /// <response code="404">If the Default Categoy was not founded</response>
        [BasicAuthentication]
        [Route("api/defaultcategories/{id:int}")]
        public IHttpActionResult GetDefaultCategory(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumber == "GATEWAY")
            {

                string queryString = "SELECT * FROM DefaultCategories WHERE Id = @id";

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
                            DefaultCategory category = new DefaultCategory()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"],
                                Type = (string)reader["Type"]
                            };
                            return Ok(category);
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
                    return Content(HttpStatusCode.NotFound, $"Default Category {id} does not exist.");
                }
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, $"Default Category {id} does not belongs to you.");
            }
        }

        public DefaultCategory GetDefaultCategoryById(int id)
        {
            string queryString = "SELECT * FROM DefaultCategories WHERE Id = @id";

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
                        DefaultCategory category = new DefaultCategory()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = (string)reader["Name"],
                            Type = (string)reader["Type"]
                        };
                        return category;
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
        /// Search for all Default Categoies based on User authenticated
        /// </summary>
        /// <returns>A list of all Default Categoies</returns>
        /// <response code="200">Returns the Default Categoies founded. Returns null if you are not authorized</response>
        [BasicAuthentication]
        [Route("api/defaultcategories")]
        public IEnumerable<DefaultCategory> GetDefaultCategories()
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumber == "GATEWAY")
            {

                string queryString = "SELECT * FROM DefaultCategories WHERE 1 = 1";
                if (HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("name") != null)
                {
                    queryString += " AND name = @name";
                }
                if (HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("type") != null)
                {
                    queryString += " AND type = @type";
                }

                List<DefaultCategory> categories = new List<DefaultCategory>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(queryString, connection);
                        if (HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("name") != null)
                        {
                            command.Parameters.AddWithValue("@name", HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("name"));
                        }
                        if (HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("type") != null)
                        {
                            command.Parameters.AddWithValue("@type", HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("type"));
                        }
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

                        connection.Close();

                    }
                    catch (Exception)
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                    return categories;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Insert Default Categoy for authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "Name": "Food",
        ///        "Type": "D"
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="category">Default Categoy to insert</param>
        /// <returns>Default Categoy inserted</returns>
        /// <response code="200">Returns the newly created Default Categoy</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/defaultcategories")]
        public IHttpActionResult PostDefaultCategory(DefaultCategory category)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumber == "GATEWAY")
            {
                string queryStringGetCategory = "SELECT * FROM DefaultCategories WHERE Name = @name AND Type = @type;";

                string queryString = "INSERT INTO DefaultCategories(Name, Type) output INSERTED.ID VALUES(@name, @type)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {

                        SqlCommand command = new SqlCommand(queryStringGetCategory, connection);
                        command.Parameters.AddWithValue("@name", category.Name);
                        command.Parameters.AddWithValue("@type", category.Type);

                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                return BadRequest($"Duplicated category with Id: {reader["Id"].ToString()}");
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

                        command = new SqlCommand(queryString, connection);

                        command.Parameters.AddWithValue("@name", category.Name);
                        command.Parameters.AddWithValue("@type", category.Type);

                        //Done Before
                        //connection.Open();

                        int result = Convert.ToInt32(command.ExecuteScalar());
                        if (result > 0)
                        {
                            return Ok(GetDefaultCategoryById(result));
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
            else
            {
                return Content(HttpStatusCode.Unauthorized, $"You cant create Default Categories.");
            }
        }

        /// <summary>
        /// Update Default Categoy of authenticated User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///        "Name": "Food",
        ///        "Type": "D"
        ///     }
        ///     
        ///     Type IN ("D", "C")
        /// </remarks>
        /// <param name="id">Default Categoy ID</param>
        /// <param name="category">Default Categoy to be updated</param>
        /// <returns>Default Categoy Updated</returns>
        /// <response code="200">Returns the updated created Default Categoy</response>
        /// <response code="401">Default Categoy does not belongs to authenticated user</response>
        /// <response code="404">If given Default Categoy not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/defaultcategories/{id:int}")]
        public IHttpActionResult PutDefaultCategory(int id, [FromBody] DefaultCategory category)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumber == "GATEWAY")
            {
                string queryStringGetCategory = "SELECT * FROM DefaultCategories WHERE Name = @name AND Type = @type;";

                string queryString = "UPDATE DefaultCategories SET Name = @name, Type = @type WHERE Id = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryStringGetCategory, connection);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            return BadRequest($"Duplicated category with Id: {reader["Id"].ToString()}");
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

                    command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);

                    try
                    {
                        //Done Before
                        //connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return Ok(GetDefaultCategoryById(id));
                        }
                        connection.Close();

                        return Content(HttpStatusCode.NotFound, $"Default Category {id} does not exist.");
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
                return Content(HttpStatusCode.Unauthorized, $"You cant update Default Categories.\nDefault Category {id} does not belongs to you.");
            }
        }

        /// <summary>
        /// Delete a category based on given ID and on User authenticated
        /// </summary>
        /// <param name="id">Default Categoy ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="401">Default Categoy does not belongs to authenticated user</response>
        /// <response code="404">If given Default Categoy not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/defaultcategories/{id:int}")]
        public IHttpActionResult DeleteDefaultCategory(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            if (phoneNumber == "GATEWAY")
            {
                string queryString = "DELETE FROM DefaultCategories WHERE Id = @id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return Ok($"Default Category {id} deleted");
                        }
                        connection.Close();

                        return Content(HttpStatusCode.NotFound, $"Default Category {id} does not exist.");
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
                return Content(HttpStatusCode.Unauthorized, $"You cant delete Default Categories.\nDefault Category {id} does not belongs to you.");
            }
        }
    }
}

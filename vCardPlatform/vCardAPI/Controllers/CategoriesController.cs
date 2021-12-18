using vCardAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace vCardAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        /// <summary>
        /// Search for a category based on given ID based on User authenticated
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category found</returns>
        /// <response code="200">Returns the Category found</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If the Category was not found</response>
        [BasicAuthentication]
        [Route("api/categories/{id:int}")]
        public IHttpActionResult GetCategory(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            Category policy = GetCategoryById(id);

            if (policy != null && policy.Owner == phoneNumber)
            {
                string queryString = "SELECT * FROM Categories WHERE Id = @id AND Owner = @owner";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            Category category = new Category()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = (string)reader["Name"],
                                Type = (string)reader["Type"],
                                Owner = (string)reader["Owner"]
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
                    return Content(HttpStatusCode.NotFound, $"Category {id} does not exist.");
                }
            }
            else
            {
                if (policy != null)
                    return Content(HttpStatusCode.NotFound, $"Category {id} does not exist.");
                
                return Content(HttpStatusCode.Unauthorized, $"Category {id} does not belongs to you.");
            }
        }

        public Category GetCategoryById(int id, string phoneNumber = null)
        {
            if (phoneNumber == null)
                phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryString = "SELECT * FROM Categories WHERE Id = @id AND Owner = @owner";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@owner", phoneNumber);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Category category = new Category()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = (string)reader["Name"],
                            Type = (string)reader["Type"],
                            Owner = (string)reader["Owner"]
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
        /// Search for all categories based on User authenticated
        /// </summary>
        /// <returns>A list of all categories</returns>
        /// <response code="200">Returns the Categories found. Returns null if you are not authorized</response>
        [BasicAuthentication]
        [Route("api/categories")]
        public IEnumerable<Category> GetCategories()
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryString = "SELECT * FROM Categories WHERE Owner = @owner";

            List<Category> categories = new List<Category>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@owner", phoneNumber);

                    connection.Open();
                    
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = (string)reader["Name"],
                            Type = (string)reader["Type"],
                            Owner = (string)reader["Owner"]
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

        /// <summary>
        /// Insert Category for authenticated User
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
        /// <param name="category">Category to insert</param>
        /// <returns>Category inserted</returns>
        /// <response code="200">Returns the newly created Category</response>
        /// <response code="400">If something went wrong with inputs</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/categories")]
        public IHttpActionResult PostCategory(Category category)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryStringGetCategory = "SELECT * FROM Categories WHERE Name = @name AND Type = @type AND Owner = @owner;";

            string queryString = "INSERT INTO Categories(Name, Type, Owner) VALUES(@name, @type, @owner);SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringGetCategory, connection);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

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
                    command.Parameters.AddWithValue("@owner", phoneNumber);

                    //Done Before
                    //connection.Open();

                    int insertedID = Convert.ToInt32(command.ExecuteScalar().ToString());
                    if (insertedID > 0)
                    {
                        return Ok(GetCategoryById(insertedID));
                    }

                    connection.Close();
                    return BadRequest("Something went wrong with your input");
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
        /// Update Category of authenticated User
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
        /// <param name="id">Category ID</param>
        /// <param name="category">Category to be updated</param>
        /// <returns>Category Updated</returns>
        /// <response code="200">Returns the updated created Category</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If given Category not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/categories/{id:int}")]
        public IHttpActionResult PutCategory(int id, [FromBody] Category category)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);
            Category policy = GetCategoryById(id);

            if (policy != null && policy.Owner == phoneNumber)
            {
                string queryStringGetCategory = "SELECT * FROM Categories WHERE Name = @name AND Type = @type AND Owner = @owner;";

                string queryString = "UPDATE Categories SET Name = @name, Type = @type WHERE Id = @id AND Owner = @owner";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand(queryStringGetCategory, connection);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

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
                    command.Parameters.AddWithValue("@owner", phoneNumber);

                    try
                    {
                        //Done Before
                        //connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return Ok(GetCategoryById(id));
                        }
                        connection.Close();

                        return Content(HttpStatusCode.NotFound, $"Category {id} does not exist.");
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
                if (policy != null)
                    return Content(HttpStatusCode.NotFound, $"Category {id} does not exist.");

                return Content(HttpStatusCode.Unauthorized, $"Category {id} does not belongs to you.");
            }
        }

        /// <summary>
        /// Delete a category based on given ID and on User authenticated
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>HTTPResponse</returns>
        /// <response code="200">Returns subjective message</response>
        /// <response code="401">Category does not belongs to authenticated user</response>
        /// <response code="404">If given Category not exist</response>
        /// <response code="500">If a fatal error eccurred</response>
        [BasicAuthentication]
        [Route("api/categories/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            Category policy = GetCategoryById(id);

            if (policy != null && policy.Owner == phoneNumber)
            {
                string queryString = "DELETE FROM Categories WHERE Id = @id AND Owner = @owner";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

                    try
                    {
                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return Ok($"Category {id} deleted");
                        }
                        connection.Close();

                        return Content(HttpStatusCode.NotFound, $"Category {id} does not exist.");
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
                if (policy != null)
                    return Content(HttpStatusCode.NotFound, $"Category {id} does not exist.");

                return Content(HttpStatusCode.Unauthorized, $"Category {id} does not belongs to you.");
            }
        }
    }
}

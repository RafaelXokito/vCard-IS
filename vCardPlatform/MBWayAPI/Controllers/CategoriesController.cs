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
    public class CategoriesController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        /// <summary>
        /// Search for a category based on given ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category founded</returns>
        [BasicAuthentication]
        [Route("api/categories/{id:int}")]
        public IHttpActionResult GetCategory(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

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
                return NotFound();
            }
        }

        /// <summary>
        /// Search for all categories
        /// </summary>
        /// <returns>A list of all categories</returns>
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

        [BasicAuthentication]
        [Route("api/categories")]
        public IHttpActionResult PostCategory(Category category)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryString = "INSERT INTO Categories(Name, Type, Owner) VALUES(@name, @type, @owner)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);
                    command.Parameters.AddWithValue("@owner", phoneNumber);

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
        [Route("api/categories/{id:int}")]
        public IHttpActionResult PutCategory(int id, [FromBody] Category category)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

            string queryString = "UPDATE Categories SET Name = @name, Type = @type WHERE Id = @id AND Owner = @owner";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", category.Name);
                command.Parameters.AddWithValue("@type", category.Type);
                command.Parameters.AddWithValue("@owner", phoneNumber);

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
        [Route("api/categories/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            string phoneNumber = UserValidate.GetUserNumberAuth(Request.Headers.Authorization);

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

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

        [Route("api/defaultcategories/{id:int}")]
        public IHttpActionResult GetDefaultCategory(int id)
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
                return NotFound();
            }
        }

        [Route("api/defaultcategories")]
        public IEnumerable<DefaultCategory> GetDefaultCategories()
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

        [Route("api/defaultcategories")]
        public IHttpActionResult PostDefaultCategory(DefaultCategory category)
        {
            string queryString = "INSERT INTO DefaultCategories(Name, Type) output INSERTED.ID VALUES(@name, @type)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);

                    connection.Open();

                    int result = Convert.ToInt32(command.ExecuteScalar());
                    if (result > 0)
                    {
                        return Ok(GetDefaultCategory(result));
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

        [Route("api/defaultcategories/{id:int}")]
        public IHttpActionResult PutDefaultCategory(int id, [FromBody] DefaultCategory category)
        {
            string queryString = "UPDATE DefaultCategories SET Name = @name, Type = @type WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", category.Name);
                command.Parameters.AddWithValue("@type", category.Type);

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

        [Route("api/defaultcategories/{id:int}")]
        public IHttpActionResult DeleteDefaultCategory(int id)
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

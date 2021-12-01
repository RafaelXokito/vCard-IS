﻿using MBWayAPI.Models;
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

        [Route("api/categories/{id:int}")]
        public IHttpActionResult GetCategory(int id)
        {
            string queryString = "SELECT * FROM Categories WHERE Id = @id";

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
                        Category category = new Category()
                        {
                            Id = (int)reader["Id"],
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

        [Route("api/categories")]
        public IEnumerable<Category> GetCategories()
        {
            string queryString = "SELECT * FROM Categories";

            List<Category> categories = new List<Category>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = new Category()
                        {
                            Id = (int)reader["Id"],
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

        [Route("api/users/{number:int}/categories")]
        public IHttpActionResult PostCategory(int number, [FromBody] Category category)
        {
            string queryString = "INSERT INTO Categories(Name, Type, Owner) VALUES(@name, @type, @owner)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@type", category.Type);
                    command.Parameters.AddWithValue("@owner", number);

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

        [Route("api/categories/{id:int}")]
        public IHttpActionResult PutCategory(int id, [FromBody] Category category)
        {
            string queryString = "UPDATE Categories SET Name = @name, Type = @type WHERE Id = @id";

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

        [Route("api/categories/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            string queryString = "DELETE FROM Categories WHERE Id = @id";

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

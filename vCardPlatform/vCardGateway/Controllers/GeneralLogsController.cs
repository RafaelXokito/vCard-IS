using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class GeneralLogsController : ApiController
    {
        string connectionString = Properties.Settings.Default.ConnStr;

        [BasicAuthentication]
        [Route("api/generallogs")]
        public IEnumerable<GeneralLog> GetGeneralLogs()
        {
            string queryString = GetFilterQueryString("SELECT * FROM GeneralLogs");

            List<GeneralLog> logs = new List<GeneralLog>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        GeneralLog log = new GeneralLog()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Type = (string)reader["Type"],
                            User = (string)reader["User"],
                            Entity = (string)reader["Entity"],
                            Status = (string)reader["Status"],
                            Message = reader["ErrorMessage"].ToString(),
                            ErrorMessage = reader["ErrorMessage"].ToString(),
                            Timestamp = (DateTime)reader["Timestamp"]
                        };

                        logs.Add(log);
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
                return logs;
            }
        }

        [BasicAuthentication]
        [Route("api/generallogs/{id:int}")]
        public IHttpActionResult GetGeneralLog(int id)
        {
            string queryString = "SELECT * FROM GeneralLogs WHERE Id = @id";

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
                        GeneralLog log = new GeneralLog()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Type = (string)reader["Type"],
                            User = (string)reader["User"],
                            Entity = (string)reader["Entity"],
                            Status = (string)reader["Status"],
                            Message = reader["ErrorMessage"].ToString(),
                            ErrorMessage = reader["ErrorMessage"].ToString(),
                            Timestamp = (DateTime)reader["Timestamp"]
                        };
                        return Ok(log);
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

        private string GetFilterQueryString(string baseQueryString)
        {
            //TODO
            return baseQueryString;
        }
    }
}

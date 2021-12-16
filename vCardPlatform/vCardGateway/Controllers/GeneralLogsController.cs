﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using uPLibrary.Networking.M2Mqtt;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class GeneralLogsController : ApiController
    {
        static string connectionString = Properties.Settings.Default.ConnStr;

        //MQTT Variables
        static bool valid = true;
        static String STR_CHANNEL_NAME = "logs";
        static MqttClient m_cClient = new MqttClient("127.0.0.1");


        [BasicAuthentication]
        [Route("api/generallogs")]
        public IEnumerable<GeneralLog> GetGeneralLogs([FromUri] Filter filter)
        {
            string queryString = GetFilterQueryString("SELECT * FROM GeneralLogs", filter);

            List<GeneralLog> logs = new List<GeneralLog>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);

                    if (filter.DateStart != null)
                    {
                        command.Parameters.AddWithValue("@datestart", DateTime.Parse(filter.DateStart));
                    }
                    if (filter.DateEnd != null)
                    {
                        command.Parameters.AddWithValue("@dateend", DateTime.Parse(filter.DateEnd));
                    }

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        GeneralLog log = new GeneralLog()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Type = (string)reader["Type"],
                            Username = (string)reader["Username"],
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
                            Username = (string)reader["Username"],
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

        public static GeneralLog PostGeneralLog(string Type, string Username, string Entity, string Status, string Message, string ErrorMessage, DateTime Timestamp)
        {
            GeneralLog generalLog = new GeneralLog { 
                Type = Type,
                Username = Username,
                Entity = Entity,
                Status = Status,
                Message = Message,
                ErrorMessage = ErrorMessage,
                Timestamp = Timestamp,
            };
            return PostGeneralLog(generalLog);
        }

        public static GeneralLog PostGeneralLog(GeneralLog general)
        {

            string queryString = @"INSERT INTO GeneralLogs
                            (Type, Username, Entity, Status, Message, ErrorMessage, Timestamp) 
                        VALUES
                            (@type, @username, @entity, @status, @message, @errormessage, @timestamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@type", general.Type);
                    command.Parameters.AddWithValue("@username", general.Username ?? "");
                    command.Parameters.AddWithValue("@entity", general.Entity ?? "");
                    command.Parameters.AddWithValue("@status", general.Status);
                    command.Parameters.AddWithValue("@message", general.Message);
                    command.Parameters.AddWithValue("@errormessage", general.ErrorMessage);
                    command.Parameters.AddWithValue("@timestamp", general.Timestamp);

                    connection.Open();

                    if (command.ExecuteNonQuery() > 0)
                    {
                        m_cClient.Connect(Guid.NewGuid().ToString());

                        if (m_cClient.IsConnected)
                        {
                            m_cClient.Publish("logs", Encoding.UTF8.GetBytes(Log.BuildMessage(general.Message, general.Status, general.Timestamp)));
                        }
                        return general;
                    }

                    connection.Close();
                    return null;
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return null;
                }
            }
        }

        private string GetFilterQueryString(string baseQueryString, Filter filter)
        {
            string queryString = baseQueryString;
            bool hasOne = false;

            if (filter.DateStart != null || filter.DateEnd != null)
            {
                queryString += " WHERE ";
                if (filter.DateStart != null)
                {
                    queryString += (hasOne ? "AND " : "") + "Timestamp >= @datestart ";
                    hasOne = true;
                }

                if (filter.DateEnd != null)
                {
                    queryString += (hasOne ? "AND " : "") + "Timestamp <= @dateend ";
                    hasOne = true;
                }
            }

            return queryString + " ORDER BY Timestamp DESC";
        }

    }
}

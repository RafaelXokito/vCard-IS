﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vCardGateway.Models;

namespace vCardGateway.Controllers
{
    public class TransactionLogsController : ApiController
    {
        static string connectionString = Properties.Settings.Default.ConnStr;

        public class Filter
        {
            public string FromEntity { get; set; }
            public string FromNewBalance { get; set; }
            public string ToEntity { get; set; }
            public bool Successful { get; set; }

            //TODO
        }

        [BasicAuthentication]
        [Route("api/transactionlogs")]
        public IEnumerable<TransactionLog> GetTransactionLogs()
        {
            string queryString = GetFilterQueryString("SELECT * FROM TransactionLogs");

            List<TransactionLog> logs = new List<TransactionLog>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(queryString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TransactionLog log = new TransactionLog()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FromUser = (string)reader["FromUser"],
                            FromEntity = (string)reader["FromEntity"],
                            ToUser = (string)reader["ToUser"],
                            ToEntity = (string)reader["ToEntity"],
                            Amount = (decimal)reader["Amount"],
                            Status = (string)reader["Status"],
                            Message = reader["Message"].ToString(),
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
        [Route("api/transactionlogs/{id:int}")]
        public IHttpActionResult GetTransactionLog(int id)
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
                        TransactionLog log = new TransactionLog()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FromUser = (string)reader["FromUser"],
                            FromEntity = (string)reader["FromEntity"],
                            ToUser = (string)reader["ToUser"],
                            ToEntity = (string)reader["ToEntity"],
                            Amount = (decimal)reader["Amount"],
                            Status = (string)reader["Status"],
                            Message = reader["Message"].ToString(),
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

        public static bool PostTransactionLog(TransactionLog transactionLog)
        {

            string queryString = @"INSERT INTO TransactionLogs
                            (FromUser, FromEntity, ToUser, ToEntity, Amount, Status, Message, ErrorMessage, Timestamp) 
                        VALUES
                            (@fromuser, @fromentity, @touser, @toentity, @amount, @status, @message, @errormessage, @timestamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@fromuser", transactionLog.FromUser);
                    command.Parameters.AddWithValue("@fromentity", transactionLog.FromEntity);
                    command.Parameters.AddWithValue("@touser", transactionLog.ToUser);
                    command.Parameters.AddWithValue("@toentity", transactionLog.ToEntity);
                    command.Parameters.AddWithValue("@amount", transactionLog.Amount);
                    command.Parameters.AddWithValue("@status", transactionLog.Status);
                    command.Parameters.AddWithValue("@message", transactionLog.Message);
                    command.Parameters.AddWithValue("@errormessage", transactionLog.ErrorMessage ?? "");
                    command.Parameters.AddWithValue("@timestamp", transactionLog.Timestamp);

                    connection.Open();

                    if (command.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }

                    connection.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return false;
                }
            }
        }
    }
}
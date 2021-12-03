using MBWayAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MBWayAPI
{
    public class UserValidate
    {
        public static bool Login(string phoneNumber, string password)
        {
            string connectionString = Properties.Settings.Default.ConnStr;

            string queryString = "SELECT Password FROM Users WHERE PhoneNumber = @number";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@number", phoneNumber);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        User user = new User()
                        {
                            Password = (string)reader["Password"]
                        };
                        return GetHash(SHA256.Create(), password) == user.Password;
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
                return false;
            }
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using vCardGateway.Models;

namespace MBWayAPI
{
    public class AdminValidate
    {
        public static bool Login(string email, string password)
        {
            string connectionString = vCardGateway.Properties.Settings.Default.ConnStr;

            string queryString = "SELECT Password FROM Administrators WHERE Email = @email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.AddWithValue("@email", email);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string atualPassword = (string)reader["Password"];

                        return GetHash(SHA256.Create(), password) == atualPassword;
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

        public static string GetAdministratorEmailAuth(AuthenticationHeaderValue authHeader)
        {
            if (authHeader == null)
            {
                return null;
            }
            string authEncoded = authHeader.ToString().Split(' ')[1];
            string authDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(authEncoded));
            return authDecoded.Split(':')[0];
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
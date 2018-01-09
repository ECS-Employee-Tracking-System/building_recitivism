using Dapper;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SeniorProjectECS.Library
{
    // A comment
    public class DBHandler
    {
        /// <summary>
        /// Get the SQL connection.
        /// </summary>
        /// <returns>A SQLConnection to a database</returns>
        public static SqlConnection GetSqlConnection()
        {
            String connectionString = LoadConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        private static String LoadConnectionString()
        {
            using (StreamReader reader = File.OpenText("connectionString.txt"))
            {
                return reader.ReadLine();
            }
        }
    }
}

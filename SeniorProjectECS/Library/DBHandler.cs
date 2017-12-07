using Dapper;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            String connectionString = "Server=tcp:ecs2.database.windows.net;Database=ESC2;User ID=ECSAdmin@ecs2.database.windows.net;Password=Testuser1;Trusted_Connection=False;Encrypt=True";
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}

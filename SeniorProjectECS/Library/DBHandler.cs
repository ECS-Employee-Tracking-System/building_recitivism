using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Library
{
    public class DBHandler
    {
        public static SqlConnection GetSqlConnection()
        {
            String connectionString = "Connection String Here";
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}

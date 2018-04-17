using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeniorProjectECS.Library;

namespace SeniorProjectECS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitPassword();
            BuildWebHost(args).Run();
        }

        public static void InitPassword()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query("SetSeedAccount", commandType: CommandType.StoredProcedure);
            }
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

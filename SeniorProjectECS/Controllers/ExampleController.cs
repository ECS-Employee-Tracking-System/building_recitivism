using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            String connectionString = "";
            using (System.Data.IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                var people = dbConnection.Query<ExampleModel>("SELECT *, Firstname+'@'+Lastname AS [FullName] FROM ExampleTable").ToList();

                ExampleModel test = new ExampleModel();
                test.FirstName = "hard coded";

                people.Add(test);

                return View(people);
            }
        }
    }
}

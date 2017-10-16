using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            var con = DBHandler.GetSqlConnection();
            var people = con.Query<ExampleModel>("SELECT *, Firstname+'@'+Lastname AS [FullName] FROM ExampleTable").ToList();

            return View(people);
        }
    }
}

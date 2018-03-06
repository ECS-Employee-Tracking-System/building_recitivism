using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;

namespace SeniorProjectECS.Controllers
{
    public class MaintenanceController : Controller
    {
        [AdminOnly]
        public IActionResult Index()
        {
            return View();
        }

        [AdminOnly]
        public IActionResult AnnualReset()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query("AnnualReset", commandType: CommandType.StoredProcedure);
                return RedirectToAction("Index");
            }
        }
    }
}
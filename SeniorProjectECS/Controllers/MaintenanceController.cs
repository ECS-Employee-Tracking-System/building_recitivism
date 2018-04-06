using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;

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
        [AdminOnly]
        public IActionResult CleanEducation()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var results =con.Query<Education>("GetEmptyEducations", commandType: CommandType.StoredProcedure);
                return View(results);
            }
        }
        [AdminOnly]
        public IActionResult DeleteEducation(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query<Education>("DELETE from Education where EducationID=@id",   new {id=id}).FirstOrDefault();
                return RedirectToAction("CleanEducation");
            }
        }
    }
}
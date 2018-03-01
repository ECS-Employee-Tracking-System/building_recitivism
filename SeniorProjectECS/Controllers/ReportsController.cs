using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateFilter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFilter(Filter Model)
        {


            return View();
        }

        public JsonResult GetSelectLists()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var dataList = con.Query("GetSelectLists", commandType: CommandType.StoredProcedure);
                return Json(dataList);
            }
        }

        public JsonResult GetFilterList()
        {
            using(var con = DBHandler.GetSqlConnection())
            {
                var filterList = con.Query("SELECT * FROM Filter");
                return Json(filterList);
            }
        }


        public IActionResult CDACompliance(int NumberOfDays = 90)
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT StaffMemberID, FirstName, LastName, Email, CDAExpiration FROM StaffMember 
                WHERE CDAExpiration is not NULL 
                and datediff(""dd"",CONVERT(date, getdate()),""CDAExpiration"") <=@NumberOfDays";

            
            var cdaexpiration = con.Query<StaffMember>(sql ,new{ NumberOfDays = NumberOfDays});
            return View(cdaexpiration);
        }//end View Index
    }
}

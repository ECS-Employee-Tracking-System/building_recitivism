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
    public class CenterController : Controller
    {
        public IActionResult Index()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = "SELECT * FROM Center";

            var centers = con.Query<Center>(sql);

            return View(centers);
        }//end View Index

        public IActionResult Details(int? id)
        {
            return View(Center.GetCenter(id));
        }//end View Details
    }
}

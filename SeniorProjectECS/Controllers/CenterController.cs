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
            var handler = new CenterHandlerDapper();
            return View(handler.GetModel(id.GetValueOrDefault()));
        }//end View Details


        // GET: Center/Create
        [AdminOnly]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Center/Create
        [HttpPost]
        [AdminOnly]
        public ActionResult Create(Center model)
        {
            var handle = new CenterHandlerDapper();
            handle.AddModel(model);

            return RedirectToAction("Index");
        }
        [AdminOnly]
        public IActionResult CleanCenters()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var results = con.Query<Center>("GetEmptyCenters", commandType: CommandType.StoredProcedure);
                return View(results);
            }
        }
        [AdminOnly]
        public IActionResult Delete(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query<Center>("DELETE from Center where CenterID=@id", new { id = id }).FirstOrDefault();
                return RedirectToAction("Index");
            }
        }
        public JsonResult GetAllCenters()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var center =con.Query<Center>(@"SELECT * from Center");
                return Json(center);
            }
        }
    }
}

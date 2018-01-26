using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Newtonsoft.Json;


namespace SeniorProjectECS.Controllers
{
    public class StaffMemberController : Controller
    {
        private int PageSize = 10;
        private int PageNumber;

        public IActionResult Index(int? page)
        {
            PageNumber = (page ?? 1);
            if(PageNumber <= 0) { PageNumber = 1; }
            ViewBag.PageNumber = PageNumber;

            var handler = new StaffHandlerDapper();
            var results = handler.GetModels();

            return View(results.ToList().ToPagedList(PageNumber, PageSize));
        }//end View Index

        public IActionResult Details(int? id)
        {     
            if(id != null)
            {
                var handle = new StaffHandlerDapper();
                var result = handle.GetModel(id.GetValueOrDefault());
                return View(result);
            } else
            {
                return View();
            }
        }//end View Details

        public IActionResult Create()
        {
            var handle = new StaffHandlerDapper();
            var result = handle.GetModel(1);
            return View();
        }

        // POST: StaffMember/Create
        [HttpPost]
        public ActionResult Create(StaffMember model)
        {
            if(model != null)
            {
                var handle = new StaffHandlerDapper();
                handle.AddModel(model);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var handle = new StaffHandlerDapper();
                var result = handle.GetModel(id.GetValueOrDefault());
                return View(result);
            }
            else
            {
                return View();
            }
        }

        // POST: StaffMember/Create
        [HttpPost]
        public ActionResult Edit(StaffMember model)
        {
            if (model != null)
            {
                var handle = new StaffHandlerDapper();
                handle.UpdateModel(model);
            }
            return RedirectToAction("Edit", new { id = model.StaffMemberID });
        }

        public IActionResult Inactive()
        {
            var handler = new StaffHandlerDapper();
            var results = handler.GetModels();

            return View(results);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var handle = new StaffHandlerDapper();
                handle.DeleteModel(id.GetValueOrDefault());
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveEducation(int? educationID, int? staffMemberID)
        {
            if(educationID != null && staffMemberID != null)
            {
                var con = DBHandler.GetSqlConnection();
                con.Execute("RemoveStaffEducation", 
                    new { StaffMemberID = staffMemberID.GetValueOrDefault(),
                    EducationID = educationID.GetValueOrDefault() }, 
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return RedirectToAction("Edit", new { id = staffMemberID.GetValueOrDefault() });
        }

        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEducation(int? staffMemberID, string degreeAbbreviation, string degreeLevel, string degreeType, string degreeDetail)
        {
            if(staffMemberID != null)
            {
                Education edu = new Education
                {
                    DegreeAbrv = degreeAbbreviation,
                    DegreeDetail = degreeDetail,
                    DegreeLevel = degreeLevel,
                    DegreeType = degreeType
                };

                var handle = new StaffHandlerDapper();
                handle.AddEducationToModel(staffMemberID.GetValueOrDefault(), edu);
            }
            return RedirectToAction("Edit", new { id = staffMemberID.GetValueOrDefault() });
        }

        //returns json to ajax call a list of all available degree abreviations
        [HttpGet]
        public JsonResult GetDegreeAbrvList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT DISTINCT DegreeAbrv FROM Education";
            var degreeAbrvlist = con.Query(sql);
            return Json(degreeAbrvlist);
        }//end GetDegreeAbrvList

        //returns json to ajax call a list of all available degree level
        [HttpGet]
        public JsonResult GetDegreeLevelList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT DISTINCT DegreeLevel FROM Education";
            var degreeLevellist = con.Query(sql);
            return Json(degreeLevellist);
        }//end GetDegreeLevelList

        //returns json to ajax call a list of all available degree type
        [HttpGet]
        public JsonResult GetDegreeTypeList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT DISTINCT DegreeType FROM Education";
            var degreeTypelist = con.Query(sql);
            return Json(degreeTypelist);
        }//end GetDegreetypeList

        //returns json to ajax call a list of all available degree detail
        [HttpGet]
        public JsonResult GetDegreeDetailList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT DISTINCT DegreeDetail FROM Education";
            var degreeDetaillist = con.Query(sql);
            return Json(degreeDetaillist);
        }//end GetDegreeDetailList

        //returns json to ajax call a list of all available positions
        [HttpGet]
        public JsonResult GetCenterList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT name FROM Center";
            var centers = con.Query(sql);
            return Json(centers);
        }//end GetCenterList

        //returns json to ajax call a list of all available centers
        [HttpGet]
        public JsonResult GetPositionList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT DISTINCT Position FROM StaffMember";
            var positions= con.Query(sql);
            return Json(positions);
        }//end GetCenterList
    }


}
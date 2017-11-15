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
    public class StaffMemberController : Controller
    {
        public IActionResult Index()
        {
            var staffMembers = StaffMember.GetStaffMembers("");

            return View(staffMembers);
        }//end View Index

        public IActionResult Details(int? id)
        {          
            return View(StaffMember.GetStaffMember(id));
        }//end View Details
        public IActionResult Index1()
        {
      
            return View(StaffMember.GetStaffMembers(""));
        }//end View Index1
        public IActionResult Index2()
        {
            StaffDBHandler dbhandle = new StaffDBHandler();
            ModelState.Clear();
            return View(dbhandle.GetStaffMember());
        }//end View Index1
        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffMember/Create
        [HttpPost]
        public ActionResult Create(StaffMember smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StaffDBHandler sdb = new StaffDBHandler();
                    if (sdb.AddStaffMember(smodel))
                    {
                        ViewBag.Message = "Staff Member Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }


}

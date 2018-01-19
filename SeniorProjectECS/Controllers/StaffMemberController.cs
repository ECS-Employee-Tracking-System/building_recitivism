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
            var handler = new StaffHandlerDapper();
            var results = handler.GetModels();

            return View(results);
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
            //return View(StaffMember.GetStaffMember(id));
        }//end View Details

        public IActionResult Create()
        {
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
            return View();
        }
    }


}

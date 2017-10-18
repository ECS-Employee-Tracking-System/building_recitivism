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
    }
}

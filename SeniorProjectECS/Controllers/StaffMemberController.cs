﻿using Dapper;
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
            var con = DBHandler.GetSqlConnection();
            // do a join to get the centers as well
            String sql = "SELECT * FROM StaffMember LEFT JOIN Center ON StaffMember.CenterID = Center.CenterID";

            // tell dapper that we want multiple objects mapped from our query (staff member and center)
            var staffMembers = con.Query<StaffMember, Center, StaffMember>(sql, (staffMember, center) => { staffMember.Center = center; return staffMember; }, splitOn: "CenterID");

            return View(staffMembers);
        }
    }
}

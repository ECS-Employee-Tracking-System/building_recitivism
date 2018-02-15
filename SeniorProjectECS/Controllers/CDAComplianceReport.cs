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
    public class CDAComplianceController : Controller
    {
        public IActionResult Index(int NumberOfDays = 90)
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

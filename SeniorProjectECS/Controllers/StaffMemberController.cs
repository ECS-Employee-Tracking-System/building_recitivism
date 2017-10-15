using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
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

            return View();
        }
    }
}

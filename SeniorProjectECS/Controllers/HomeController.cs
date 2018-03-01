using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Models;
using SeniorProjectECS.Library;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace SeniorProjectECS.Controllers
{
    public class HomeController : Controller
    {
        [DefaultAction]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [DefaultAction]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [DefaultAction]
        public IActionResult Login(User LoginAttempt)
        {
            if(LoginAttempt != null)
            {
                using(var con = DBHandler.GetSqlConnection())
                {
                    String sql = "SELECT * FROM ECSUser WHERE Email=@AttemptEmail";
                    var data = con.Query<User>(sql, new { AttemptEmail = LoginAttempt.Email });

                    if(data.Count() > 0)
                    {
                        if(BCrypt.Net.BCrypt.Verify(LoginAttempt.PasswordHash, data.First().PasswordHash))
                        {
                            HttpContext.Session.SetInt32("AccessLevel", data.First().AccessLevel);
                        }
                    }

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}

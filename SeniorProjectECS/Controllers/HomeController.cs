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
            ViewBag.loginStatus = HttpContext.Session.GetString("LoginStatus");
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

                    if((data.Count() > 0 ) && ((LoginAttempt.PasswordHash) != null))
                    {
                        if(BCrypt.Net.BCrypt.Verify(LoginAttempt.PasswordHash, data.First().PasswordHash))
                        {
                            HttpContext.Session.SetInt32("AccessLevel", data.First().AccessLevel);
                            HttpContext.Session.SetString("LogUserName", data.First().FirstName);

                            if (HttpContext.Session.GetInt32("AccessLevel") == 0)
                            {
                                HttpContext.Session.SetString("AccessRole", "Seed");
                            } else if (HttpContext.Session.GetInt32("AccessLevel") == 1) {
                                HttpContext.Session.SetString("AccessRole", "Admin");
                            } else if (HttpContext.Session.GetInt32("AccessLevel") == 2) {
                                HttpContext.Session.SetString("AccessRole", "View Only User");
                            }
                            var user = HttpContext.Session.GetString("LogUserName");
                            HttpContext.Session.SetString("LoginStatus", user + " is Logged in");
                            return RedirectToAction("Index", "StaffMember");
                        }
                    }
                    HttpContext.Session.SetString("LoginStatus", "Login Failed, Please Try Again");
                    return RedirectToAction("Index");
                }
            }
            HttpContext.Session.SetString("LoginStatus", "Login Failed, Please Try Again");
            return RedirectToAction("Index");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }

}

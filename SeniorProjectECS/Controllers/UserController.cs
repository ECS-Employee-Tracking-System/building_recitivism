using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Models;
using BCrypt;
using SeniorProjectECS.Library;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace SeniorProjectECS.Controllers
{
    public class UserController : Controller
    {
        [AdminOnly]
        public IActionResult Index()
        {
            var handler = new UserHandlerDapper();
            var results = handler.GetModels();
            return View(results);
        }

        [AdminOnly]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult Create(User Model)
        {
            if (Model != null)
            {
                Model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Model.PasswordHash);
                var handle = new UserHandlerDapper();
                handle.AddModel(Model);
            }
            return RedirectToAction("Index");
        }

        [AdminOnly]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var handle = new UserHandlerDapper();
                var result = handle.GetModel(id.GetValueOrDefault());
                return View(result);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult Edit(User Model)
        {
            if(Model != null)
            {
                var handle = new UserHandlerDapper();
                handle.UpdateModel(Model);
            }

            return RedirectToAction("Index");
        }

        [ViewOnly]
        public IActionResult ResetPassword()
        {
            int? id = HttpContext.Session.GetInt32("UserID");
            if (id != null)
            {
                var handle = new UserHandlerDapper();
                var result = handle.GetModel(id.GetValueOrDefault());
                return View(result);
            }

            return RedirectToAction("Index");
        }
        [AdminOnly]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var handle = new UserHandlerDapper();
                handle.DeleteModel(id.GetValueOrDefault());
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult ChangePassword(int? id, String PasswordHash)
        {
            if (id != null && PasswordHash != null)
            {
                using (var con = DBHandler.GetSqlConnection())
                {
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordHash);
                    String sql = "UPDATE ECSUser SET PasswordHash=@Password WHERE UserID=@id";
                    con.Query(sql, new { id = id.GetValueOrDefault(), Password = PasswordHash });
                }
            }

            return RedirectToAction("Index");
        }
    }
}
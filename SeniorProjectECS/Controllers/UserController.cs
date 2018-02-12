using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Models;

namespace SeniorProjectECS.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var handler = new UserHandlerDapper();
            var results = handler.GetModels();
            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User Model)
        {
            if (Model != null)
            {
                var handle = new UserHandlerDapper();
                handle.AddModel(Model);
            }
            return RedirectToAction("Index");
        }

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
        public IActionResult Edit(User Model)
        {
            if(Model != null)
            {
                var handle = new UserHandlerDapper();
                handle.UpdateModel(Model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var handle = new UserHandlerDapper();
                handle.DeleteModel(id.GetValueOrDefault());
            }

            return RedirectToAction("Index");
        }
    }
}
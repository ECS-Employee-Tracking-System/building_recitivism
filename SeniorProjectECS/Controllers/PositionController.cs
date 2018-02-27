using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Models;

namespace SeniorProjectECS.Controllers
{
    public class PositionController : Controller
    {
        // GET: Position
        public ActionResult Index()
        {
            var handle = new PositionHandlerDapper();

            return View(handle.GetModels());
        }

        // GET: Position/Details/5
        public ActionResult Details(int id)
        {
            var handle = new PositionHandlerDapper();

            return View(handle.GetModel(id));
        }

        // GET: Position/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Position/Create
        [HttpPost]
        public ActionResult Create(Position model)
        {
            var handle = new PositionHandlerDapper();
            handle.AddModel(model);

            return RedirectToAction("Index");
        }

        // GET: Position/Edit/5
        public ActionResult Edit(int id)
        {
            var handle = new PositionHandlerDapper();

            return View(handle.GetModel(id));
        }

        // POST: Position/Edit/5
        [HttpPost]
        public ActionResult Edit(Position model)
        {
            var handle = new PositionHandlerDapper();
            handle.UpdateModel(model);

            return RedirectToAction("Index");
        }
    }
}
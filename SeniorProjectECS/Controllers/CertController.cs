﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using Dapper;
using SeniorProjectECS.Models;

namespace SeniorProjectECS.Controllers
{
    public class CertController : Controller
    {
        // GET: Cert
        public ActionResult Index()
        {
            var handle = new CertHandlerDapper();

            return View(handle.GetModels());
        }

        // GET: Cert/Details/5
        public ActionResult Details(int id)
        {
            var handle = new CertHandlerDapper();

            return View(handle.GetModel(id));
        }

        // GET: Cert/Create
        [AdminOnly]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cert/Create
        [HttpPost]
        [AdminOnly]
        public ActionResult Create(Certification model)
        {
            var handle = new CertHandlerDapper();
            handle.AddModel(model);

            return RedirectToAction("Index");
        }

        // GET: Cert/Edit/5
        [AdminOnly]
        public ActionResult Edit(int id)
        {
            var handle = new CertHandlerDapper();

            return View(handle.GetModel(id));
        }

        // POST: Cert/Edit/5
        [HttpPost]
        [AdminOnly]
        public ActionResult Edit(Certification model)
        {
            var handle = new CertHandlerDapper();
            handle.UpdateModel(model);

            return RedirectToAction("Index");
        }

        // GET: Cert/Delete/5
        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var handle = new CertHandlerDapper();
            handle.DeleteModel(id);

            return RedirectToAction("Index");
        }

        //returns json to ajax call a list of all available certifications
        [HttpGet]
        public JsonResult GetCertificationList()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = "SELECT * FROM Certification";
            var certList = con.Query(sql);
            return Json(certList);
        }//end GetDegreeAbrvList
    }
}
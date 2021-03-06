﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Models;
using SeniorProjectECS.Library;
using Dapper;
using System.Data;

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
        [AdminOnly]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Position/Create
        [HttpPost]
        [AdminOnly]
        public ActionResult Create(Position model)
        {
            var handle = new PositionHandlerDapper();
            handle.AddModel(model);

            return RedirectToAction("Index");
        }

        // GET: Position/Edit/5
        [AdminOnly]
        public ActionResult Edit(int id)
        {
            var handle = new PositionHandlerDapper();

            return View(handle.GetModel(id));
        }

        // POST: Position/Edit/5
        [HttpPost]
        [AdminOnly]
        public ActionResult Edit(Position model)
        {
            var handle = new PositionHandlerDapper();
            handle.UpdateModel(model);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Remove a certificate from being required by a position.
        /// </summary>
        /// <param name="CertificationID">The certification to remove.</param>
        /// <param name="PositionID">The position to modify.</param>
        /// <returns></returns>
        [AdminOnly]
        public IActionResult RemoveCertification(int? CertificationID, int? PositionID)
        {
            if(CertificationID != null && PositionID != null)
            {
                using (var con = DBHandler.GetSqlConnection())
                {
                    String sql = "DELETE FROM PositionReq WHERE PositionID=@PosID AND CertificationID=@CertID";
                    con.Execute(sql, new { PosID = PositionID, CertID = CertificationID });
                }
            }

            return RedirectToAction("Edit", new { id = PositionID.GetValueOrDefault() });
        }

        /// <summary>
        /// Add a new required certificate to the position.
        /// </summary>
        /// <param name="PositionID">The ID of the position to change.</param>
        /// <param name="CertificationID">The ID of the certification to add.</param>
        /// <returns></returns>
        [AdminOnly]
        public ActionResult AddRequiredCert(int? PositionID, int? CertificationID)
        {
            if(PositionID != null && CertificationID != null)
            {
                using(var con = DBHandler.GetSqlConnection())
                {
                    String sql = "INSERT INTO PositionReq (PositionID, CertificationID) VALUES (@PosID, @CertID)";
                    try {
                        con.Execute(sql, new { PosID = PositionID.GetValueOrDefault(), CertID = CertificationID.GetValueOrDefault() });
                    } catch (System.Data.SqlClient.SqlException e) { } 
                }
            }

            return RedirectToAction("Edit", new { id = PositionID.GetValueOrDefault() });
        }
        [AdminOnly]
        public IActionResult CleanPosition()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var results = con.Query<Position>("GetEmptyPositions", commandType: CommandType.StoredProcedure);
                return View(results);
            }
        }
        [AdminOnly]
        public IActionResult Delete(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query<Center>("DELETE from Position where PositionID=@id", new { id = id }).FirstOrDefault();
                return RedirectToAction("Index");
            }
        }
    }
}
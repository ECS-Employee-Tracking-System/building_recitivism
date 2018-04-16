using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace SeniorProjectECS.Controllers
{
    public class StaffMemberController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffMember/Create
        [HttpPost]
        [AdminOnly]
        public ActionResult Create(StaffMember model)
        {
            if (model != null)
            {
                var handle = new StaffHandlerDapper();
                handle.AddModel(model);
            }
            return RedirectToAction("List", "Reports");
        }

        [AdminOnly]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var handle = new StaffHandlerDapper();
                var result = handle.GetModel(id.GetValueOrDefault());
                return View(result);
            }
            else
            {
                return RedirectToAction("List", "Reports");
            }
        }

        // POST: StaffMember/Create
        [HttpPost]
        [AdminOnly]
        public ActionResult Edit(StaffMember model)
        {
            if (model != null)
            {
                var handle = new StaffHandlerDapper();
                handle.UpdateModel(model);
            }
            return RedirectToAction("List", "Reports");
        }

        public IActionResult Inactive()
        {
            var handler = new StaffHandlerDapper();
            var results = handler.GetModels();

            return View(results);
        }

        [AdminOnly]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var handle = new StaffHandlerDapper();
                handle.DeleteModel(id.GetValueOrDefault());
            }
            return RedirectToAction("List", "Reports");
        }

        [AdminOnly]
        public IActionResult RemoveEducation(int? educationID, int? staffMemberID)
        {
            if (educationID != null && staffMemberID != null)
            {
                var con = DBHandler.GetSqlConnection();
                con.Execute("RemoveStaffEducation",
                    new { StaffMemberID = staffMemberID.GetValueOrDefault(),
                        EducationID = educationID.GetValueOrDefault() },
                    commandType: System.Data.CommandType.StoredProcedure
                    );
            }
            return RedirectToAction("Edit", new { id = staffMemberID.GetValueOrDefault() });
        }

        [AdminOnly]
        public IActionResult RemovePosition(int? staffMemberID, int? positionID)
        {
            if (staffMemberID != null && positionID != null)
            {
                using (var con = DBHandler.GetSqlConnection())
                {
                    con.Execute("RemovePosition",
                        new { StaffMemberID = staffMemberID.GetValueOrDefault(),
                            PositionID = positionID.GetValueOrDefault() },
                        commandType: System.Data.CommandType.StoredProcedure
                        );
                }
            }
            return RedirectToAction("Edit", new { id = staffMemberID.GetValueOrDefault() });
        }

        [AdminOnly]
        public IActionResult RemoveCertification(int? StaffMemberID, int? certificationID)
        {
            if(StaffMemberID != null && certificationID != null)
            {
                using(var con = DBHandler.GetSqlConnection())
                {
                    String sql = "DELETE CertCompletion WHERE StaffMemberID=@StaffID AND CertificationID=@CertID";
                    con.Execute(sql, new { StaffID = StaffMemberID.GetValueOrDefault(), CertID = certificationID.GetValueOrDefault() });
                }
            }

            return RedirectToAction("Edit", new { id = StaffMemberID.GetValueOrDefault() });
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddPosition(int? staffMemberID, String positionTitle)
        {
            if(staffMemberID != null)
            {
                using (var con = DBHandler.GetSqlConnection())
                {
                    con.Execute("AddNewPosition", new { StaffMemberID = staffMemberID, PositionTitle = positionTitle }, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            return RedirectToAction("Edit", new { id = staffMemberID.GetValueOrDefault() });
        }

        [AdminOnly]
        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        [AdminOnly]
        public IActionResult AddEducation(int? staffMemberID, string degreeAbbreviation, string degreeLevel, string degreeType, string degreeDetail)
        {
            if(staffMemberID != null)
            {
                Education edu = new Education
                {
                    DegreeAbrv = degreeAbbreviation,
                    DegreeDetail = degreeDetail,
                    DegreeLevel = degreeLevel,
                    DegreeType = degreeType
                };

                var handle = new StaffHandlerDapper();
                handle.AddEducationToModel(staffMemberID.GetValueOrDefault(), edu);
            }
            return RedirectToAction("Edit", new { id = staffMemberID.GetValueOrDefault() });
        }

        [AdminOnly]
        public IActionResult AddCompletedCert(int? StaffMemberID, int? CertificationID, DateTime? DateCompleted, bool CertInProgress = false)
        {
            if(StaffMemberID != null && CertificationID != null)
            {
                using(var con = DBHandler.GetSqlConnection())
                {
                    String sql = "INSERT INTO CertCompletion (StaffMemberID, CertificationID, CertCompletionDate, CertInProgress) VALUES (@StaffID, @CertID, @DateCompleted, @CertInProgress)";
                    try { 
                        con.Execute(sql, new { StaffID = StaffMemberID, CertID = CertificationID, DateCompleted, CertInProgress });
                    } catch (System.Data.SqlClient.SqlException e) { }
                }
            }

            return RedirectToAction("Edit", new { id = StaffMemberID.GetValueOrDefault() });
        }
    }


}
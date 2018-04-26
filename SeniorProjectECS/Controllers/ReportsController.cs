using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace SeniorProjectECS.Controllers
{
    public class ReportsController : Controller
    {
        [ViewOnly]
        public ActionResult Index()
        {
            return View();
        }

        [AdminOnly]
        public ActionResult CreateFilter()
        {
            return View();
        }

        [ViewOnly]
        public ActionResult LoadFilter(int? filterID)
        {
            if(filterID != null)
            {
                var handle = new FilterHandlerJSON();
                Filter filter = handle.GetModel(filterID.Value);
                return RedirectToAction("ApplyFilter", filter);
            }

            return RedirectToAction("ApplyFilter", new Filter());
        }

        [ViewOnly]
        public ActionResult ApplyFilter(Filter Model)
        {
            ViewBag.LoggedUser = HttpContext.Session.GetString("LogUserName");
            ViewBag.AccessRole = HttpContext.Session.GetString("AccessRole");
            String sql = BuildSQLFromFilter(Model);
            var parameters = BuildParamsFromFilter(Model);

            var returnModel = new StaffFilterViewModel();
            using (var con = DBHandler.GetSqlConnection())
            {
                var staffMembers = new List<StaffMember>();
                con.Query<StaffMember, Position, Center, Education, CertCompletion, Certification, string, StaffMember>(sql, (staff, pos, center, edu, certCompleted, cert, requiredCerts) =>
                {
                    int foundStaff = staffMembers.FindIndex(s => s.StaffMemberID == staff.StaffMemberID);
                    if (foundStaff == -1)
                    {
                        if (pos != null) { staff.Positions.Add(pos); }
                        if (center != null) { staff.Center = center; }
                        if (edu != null) { staff.Education.Add(edu); }
                        if(requiredCerts != null)
                        {
                            var reqCerts = new List<int>();
                            var temp = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(requiredCerts);
                            temp.ForEach(f => reqCerts.Add(f.FirstOrDefault().Value));

                            foreach (int reqCert in reqCerts)
                            {                   
                                staff.CompletedCerts.Add(new CertCompletion
                                {
                                    Cert = new Certification { CertificationID = reqCert },
                                    IsRequired = true,
                                    CertInProgress = false
                                });
                            }
                        }

                        if (certCompleted != null && cert != null)
                        {
                            bool found = false;
                            for(int i=0; i<staff.CompletedCerts.Count; i++)
                            {
                                if(staff.CompletedCerts[i].Cert.CertificationID == cert.CertificationID)
                                {
                                    staff.CompletedCerts[i].Cert = cert;
                                    staff.CompletedCerts[i].DateCompleted = certCompleted.DateCompleted;
                                    staff.CompletedCerts[i].CertInProgress = certCompleted.CertInProgress;
                                    if(certCompleted.DateCompleted.HasValue)
                                    {
                                        staff.CompletedCerts[i].ExpireDate = certCompleted.DateCompleted.Value.AddMonths(cert.CertExpireAmount);
                                        staff.CompletedCerts[i].DaysUntilExpire = (staff.CompletedCerts[i].ExpireDate - DateTime.Now).Value.Days;
                                    }
                                    
                                    found = true;
                                    break;
                                }
                            }

                            if(!found)
                            {
                                certCompleted.Cert = cert;

                                if(certCompleted.DateCompleted.HasValue)
                                {
                                    certCompleted.ExpireDate = certCompleted.DateCompleted.Value.AddMonths(cert.CertExpireAmount);
                                    certCompleted.DaysUntilExpire = (certCompleted.ExpireDate - DateTime.Now).Value.Days;
                                }
                                
                                certCompleted.IsRequired = false;
                                staff.CompletedCerts.Add(certCompleted);
                            }
                        }
                        staffMembers.Add(staff);
                    }
                    else
                    {
                        if (pos != null && !staffMembers[foundStaff].Positions.Any(p => p.PositionID == pos.PositionID))
                        {
                            staffMembers[foundStaff].Positions.Add(pos);
                        }

                        if (edu != null && !staffMembers[foundStaff].Education.Any(e => e.EducationID == edu.EducationID))
                        {
                            staffMembers[foundStaff].Education.Add(edu);
                        }

                        if (requiredCerts != null)
                        {
                            var reqCerts = new List<int>();
                            var temp = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(requiredCerts);
                            temp.ForEach(f => reqCerts.Add(f.FirstOrDefault().Value));

                            foreach (int reqCert in reqCerts)
                            {
                                if (!staffMembers[foundStaff].CompletedCerts.Any(rq => rq.Cert.CertificationID == reqCert))
                                {
                                    staffMembers[foundStaff].CompletedCerts.Add(new CertCompletion
                                    {
                                        Cert = new Certification { CertificationID = reqCert },
                                        IsRequired = true,
                                        CertInProgress = false
                                    });
                                }
                                else
                                {
                                    for (int i = 0; i < staffMembers[foundStaff].CompletedCerts.Count; i++)
                                    {
                                        if (staffMembers[foundStaff].CompletedCerts[i].Cert.CertificationID == reqCert)
                                        {
                                            staffMembers[foundStaff].CompletedCerts[i].IsRequired = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (certCompleted != null && cert != null)
                        {
                            bool found = false;
                            
                            for (int i = 0; i < staffMembers[foundStaff].CompletedCerts.Count; i++)
                            {
                                if (staffMembers[foundStaff].CompletedCerts[i].Cert.CertificationID == cert.CertificationID)
                                {
                                    staffMembers[foundStaff].CompletedCerts[i].Cert = cert;
                                    staffMembers[foundStaff].CompletedCerts[i].DateCompleted = certCompleted.DateCompleted;
                                    staffMembers[foundStaff].CompletedCerts[i].CertInProgress = certCompleted.CertInProgress;
                                    if(certCompleted.DateCompleted.HasValue)
                                    {
                                        staffMembers[foundStaff].CompletedCerts[i].ExpireDate = certCompleted.DateCompleted.Value.AddMonths(cert.CertExpireAmount);
                                        staffMembers[foundStaff].CompletedCerts[i].DaysUntilExpire = (staffMembers[foundStaff].CompletedCerts[i].ExpireDate - DateTime.Now).Value.Days;
                                    }
                                    
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                            {
                                certCompleted.Cert = cert;
                                if(certCompleted.DateCompleted.HasValue)
                                {
                                    certCompleted.ExpireDate = certCompleted.DateCompleted.Value.AddMonths(cert.CertExpireAmount);
                                    certCompleted.DaysUntilExpire = (certCompleted.ExpireDate - DateTime.Now).Value.Days;
                                }
                                
                                certCompleted.IsRequired = false;
                                staffMembers[foundStaff].CompletedCerts.Add(certCompleted);
                            }
                        }
                    }
                    return staff;
                }, splitOn: "PositionID,CenterID,EducationID,CertInProgress,CertificationID,RequiredCerts", param: parameters);

                staffMembers = CodeFilter(staffMembers, Model);

                returnModel.StaffMembers = staffMembers;
                returnModel.Filter = Model;
            }

            return View("Details", returnModel);
        }

        private List<StaffMember> CodeFilter(List<StaffMember> staffMembers, Filter filter)
        {
            // filter on days until expire
            if(filter.TimeUntilExpire != null)
            {
                staffMembers.RemoveAll(x => x.CompletedCerts.All(c =>
                    c.DaysUntilExpire != null &&
                    c.DaysUntilExpire > filter.TimeUntilExpire
                ));
            }

            return staffMembers;
        }

        [AdminOnly]
        public ActionResult EditFilter(Filter model)
        {
            return View(model);
        }

        [AdminOnly]
        public ActionResult SaveFilter(StaffFilterViewModel model)
        {
            var handle = new FilterHandlerJSON();
            if (model.Filter.FilterID == null || (ValidateSave(model.Filter.FilterName) as ContentResult).Content.Equals("true"))
            {
                handle.AddModel(model.Filter);
            } else {
                handle.UpdateModel(model.Filter);
            }

            return RedirectToAction("ApplyFilter", model.Filter);
        }

        [AdminOnly]
        public ActionResult DeleteFilter(int? id)
        {
            if(id.HasValue)
            {
                var handle = new FilterHandlerJSON();
                handle.DeleteModel(id.Value);
            }

            return RedirectToAction("LoadFilter");
        }

        /// <summary>
        /// Build a parameter object for use with SQL
        /// </summary>
        /// <param name="model">The model to build from.</param>
        /// <returns>An object containing all needed parameters</returns>
        private object BuildParamsFromFilter(Filter model)
        {
            dynamic parameters = new ExpandoObject();

            parameters = AddArrayToExpando(parameters, "FirstName", model.FirstName);
            parameters = AddArrayToExpando(parameters, "LastName", model.LastName);
            parameters = AddArrayToExpando(parameters, "Email", model.Email);
            parameters = AddPropertyToExpando(parameters, "Goal", model.Goal);
            parameters = AddPropertyToExpando(parameters, "MidYear", model.MidYear);
            parameters = AddPropertyToExpando(parameters, "EndYear", model.EndYear);
            parameters = AddPropertyToExpando(parameters, "GoalMet", model.GoalMet);
            parameters = AddPropertyToExpando(parameters, "TAndAApp", model.TAndAApp);
            parameters = AddPropertyToExpando(parameters, "AppApp", model.AppApp);
            parameters = AddPropertyToExpando(parameters, "ClassCompleted", model.ClassCompleted);
            parameters = AddPropertyToExpando(parameters, "ClassPaid", model.ClassPaid);
            //parameters = AddPropertyToExpando(parameters, "IsInactive", model.IsInactive);
            if(model.IsInactive == true) { parameters = AddPropertyToExpando(parameters, "IsInactive", 1); }
            else { parameters = AddPropertyToExpando(parameters, "IsInactive", 0); }

            parameters = AddArrayToExpando(parameters, "CertName", model.CertCompleted);
            parameters = AddArrayToExpando(parameters, "PositionTitle", model.Position);
            parameters = AddArrayToExpando(parameters, "DegreeLevel", model.EducationLevel);
            parameters = AddArrayToExpando(parameters, "DegreeType", model.EducationType);
            parameters = AddArrayToExpando(parameters, "DegreeDetail", model.EducationDetail);
            parameters = AddArrayToExpando(parameters, "Name", model.CenterName);
            parameters = AddArrayToExpando(parameters, "County", model.CenterCounty);
            parameters = AddArrayToExpando(parameters, "Region", model.CenterRegion);

            return parameters;
        }

        /// <summary>
        /// Add parameters to an object from an array.
        /// </summary>
        /// <param name="expando">The object to add to.</param>
        /// <param name="propertyName">The name of the parameter. ie. FirstName</param>
        /// <param name="list">The array to build the parameters from.</param>
        /// <returns>The complete parameter object.</returns>
        private object AddArrayToExpando(ExpandoObject expando, String propertyName, List<String> list)
        {
            var expandoDic = expando as IDictionary<String, object>;

            for(int i=0; i<list.Count; i++)
            {
                expandoDic.Add(propertyName + i, list[i]);
            }

            return expandoDic;
        }

        /// <summary>
        /// Add a single parameter to a parameter object.
        /// </summary>
        /// <param name="expando">The object to add to.</param>
        /// <param name="propertyName">The name of the parameter. ie. FirstName</param>
        /// <param name="item">The parameter to add.</param>
        /// <returns>The complete parameter object.</returns>
        private object AddPropertyToExpando(ExpandoObject expando, String propertyName, object item)
        {
            var expandoDic = expando as IDictionary<String, object>;

            if (item != null) {
                expandoDic.Add(propertyName, item);
            }

            return expandoDic;
        }

        /// <summary>
        /// Build a SQL query based on a filter.
        /// </summary>
        /// <param name="model">The filter to apply.</param>
        /// <returns>The completed SQL string.</returns>
        private string BuildSQLFromFilter(Filter model)
        {
            String sql = "Select sm.StaffMemberID, sm.FirstName, sm.LastName, sm.Email, sm.DateofHire, sm.DirectorCredentials, sm.DCExpiration, sm.CDAInProgress, sm.CDAType, " +
                         "sm.CDAExpiration,sm.CDARenewalProcess,sm.Comments,sm.Goal,sm.MidYear,sm.EndYear,sm.GoalMet,sm.TAndAApp,sm.AppApp,sm.ClassCompleted,sm.ClassPaid, " +
                         "sm.RequiredHours,sm.HoursEarned,sm.Notes, sm.TermDate,sm.IsInactive, p.PositionID,p.PositionTitle, c.CenterID, c.Name, c.County, c.Region, e.EducationID,e.DegreeAbrv, " +
                         "e.DegreeLevel, e.DegreeType, e.DegreeDetail, cc.CertInProgress, cc.CertCompletionDate as DateCompleted, cert.CertificationID, cert.CertName, cert.CertExpireAmount, " +
                         "(select c.CertificationID from Certification as c " +
                         "inner join PositionReq as pr on pr.CertificationID = c.CertificationID " +
                         "inner join Position as pos on pos.PositionID = pr.PositionID " +
                         "where pos.PositionID like p.PositionID for json path) as RequiredCerts " +
                         "FROM StaffMember as sm " +
                         "left Outer JOIN StaffPosition as sp on sp.StaffMemberID=sm.StaffMemberID " +
                         "left Outer JOIN Position as p on p.PositionID = sp.PositionID " +
                         "Left Outer JOIN Center as c on c.CenterID = sm.CenterID " +
                         "Left Outer JOIN StaffEducation as se on se.StaffMemberID = sm.StaffMemberID " +
                         "Left Outer JOIN Education as e on e.educationID = se.EducationID " +
                         "Left Outer JOIN CertCompletion as cc on cc.StaffMemberID = sm.StaffMemberID " +
                         "Left Outer JOIN Certification as cert on cert.CertificationID = cc.CertificationID " +
                         "WHERE (sm.StaffMemberID like '%')";

            sql = BuildSQLFromArray(sql, model.FirstName, "FirstName", "sm");
            sql = BuildSQLFromArray(sql, model.LastName, "LastName", "sm");
            sql = BuildSQLFromArray(sql, model.Email, "Email", "sm");
            sql = HandleDate(sql, model.BeginDateOfHire, model.EndDateOfHire, "DateOfHire");
            if(model.Goal != null) { sql += " AND (sm.Goal=@Goal)"; }
            if (model.MidYear != null) { sql += " AND (sm.MidYear=@MidYear)"; }
            if (model.EndYear != null) { sql += " AND (sm.EndYear=@EndYear)"; }
            if (model.GoalMet != null) { sql += " AND (sm.GoalMet=@GoalMet)"; }
            if (model.TAndAApp != null) { sql += " AND (sm.TAndAApp=@TAndAApp)"; }
            if (model.AppApp != null) { sql += " AND (sm.AppApp=@AppApp)"; }
            if (model.ClassCompleted != null) { sql += " AND (sm.ClassCompleted=@ClassCompleted)"; }
            if (model.ClassPaid != null) { sql += " AND (sm.ClassPaid=@ClassPaid)"; }
            sql = HandleNum(sql, model.BeginRequiredHours, model.EndRequiredHours, "RequiredHours");
            sql = HandleNum(sql, model.BeginHoursEarned, model.EndHoursEarned, "HoursEarned");
            sql = HandleDate(sql, model.BeginTermDate, model.EndTermDate, "TermDate");
            sql += " AND (sm.IsInactive=@IsInactive)";
            sql = BuildSQLFromArray(sql, model.CertCompleted, "CertName", "cert");
            sql = BuildSQLFromArray(sql, model.Position, "PositionTitle", "p");
            sql = BuildSQLFromArray(sql, model.EducationLevel, "DegreeLevel", "e");
            sql = BuildSQLFromArray(sql, model.EducationType, "DegreeType", "e");
            sql = BuildSQLFromArray(sql, model.EducationDetail, "DegreeDetail", "e");
            sql = BuildSQLFromArray(sql, model.CenterName, "Name", "c");
            sql = BuildSQLFromArray(sql, model.CenterCounty, "County", "c");
            sql = BuildSQLFromArray(sql, model.CenterRegion, "Region", "c");

            return sql;
        }

        /// <summary>
        /// Add WHERE clause to search on date
        /// </summary>
        /// <param name="sql">The base SQL string. This should be a valid SQL statement with a WHERE clause at the end.</param>
        /// <param name="beginDate">The starting date. We will search on entries AFTER this date. Can be null.</param>
        /// <param name="endDate">The ending date. We will search on entries BEFORE this date. Can be null.</param>
        /// <param name="name">The name of the column.</param>
        /// <returns></returns>
        private string HandleDate(string sql, DateTime? beginDate, DateTime? endDate, string name)
        {
            // Get entries after the begin date
            if(beginDate != null)
            {
                sql += "AND (" + name + " >= '" + beginDate + "')";
            }

            // Get entries before the end date
            if(endDate != null)
            {
                sql += "AND (" + name + " <= '" + endDate + "')";
            }

            return sql;
        }

        private string HandleNum(string sql, int? beginNum, int? endNum, string name)
        {
            // Get entries after the begin date
            if (beginNum != null)
            {
                sql += "AND (" + name + " >= '" + beginNum + "')";
            }

            // Get entries before the end date
            if (endNum != null)
            {
                sql += "AND (" + name + " <= '" + endNum + "')";
            }

            return sql;
        }

        /// <summary>
        /// Add WHERE clauses to a SQL string from an array.
        /// </summary>
        /// <param name="sql">The base SQL string. This should be a valid SQL statement with a WHERE clause at the end.</param>
        /// <param name="list">The list of strings to add to the query.</param>
        /// <param name="name">The base name of the column.</param>
        /// <param name="tableName">The name of the join table.</param>
        /// <returns>The original sql strings with the additional where statements appended to the end.</returns>
        private String BuildSQLFromArray(String sql, List<String> list, String name, String tableName)
        {
            if(list.Count > 0 && list[0] != null)
            {
                sql += " AND (";
                for(int i=0; i<list.Count; i++)
                {
                    sql += tableName + "." + name + " LIKE @" + name + i;
                    if(i+1 < list.Count)
                    {
                        sql += " OR ";
                    }
                }
                sql += ")";
            }

            return sql;
        }

        public ActionResult ValidateSave(string filterName)
        {
            if(FilterHandlerJSON.FilterList.ContainsValue(filterName))
            {
                return Content("false");
            }

            return Content("true");
        }

        //gets all the data to prepare the select list called in reports.js
        public JsonResult GetSelectLists()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var dataList = con.Query("GetSelectLists", commandType: CommandType.StoredProcedure);
                return Json(dataList);
            }
        }

        public JsonResult GetFilterList()
        {
            return Json(FilterHandlerJSON.FilterList);
        }

        //gets all the information to pass to the dashboard in JSON format
        [ViewOnly]
        public JsonResult GetDashBoardLists()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var dataList = con.Query<StaffMember, string, string, string, string, StaffMember>("GetDashBoardLists", (staffMember, center, edu, pos, cert) =>
                {
                    if (center != null) { staffMember.Center = JsonConvert.DeserializeObject<Center>(center); }
                    if (edu != null) { staffMember.Education = JsonConvert.DeserializeObject<List<Education>>(edu); }
                    if (pos != null) { staffMember.Positions = JsonConvert.DeserializeObject<List<Position>>(pos); }
                    if (cert != null) { staffMember.CompletedCerts = JsonConvert.DeserializeObject<List<CertCompletion>>(cert); }

                    return staffMember;
                }, splitOn: "Center,Education,Position,Cert", commandType: CommandType.StoredProcedure);
                return Json(dataList);
            }
        }

        //used in reports.js to display CertID and CertName in select list 
        public JsonResult GetCertList()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = @"select distinct(CertName), CertificationID from Certification ";
                var certs = con.Query(sql);
                return Json(certs);
            }
        }


        //used to display current Dashboard
        [ViewOnly]
        public IActionResult List()
        {
            ViewBag.LoggedUser = HttpContext.Session.GetString("LogUserName");
            ViewBag.AccessRole = HttpContext.Session.GetString("AccessRole");
            return RedirectToAction("LoadFilter");
        }
            
    }
}

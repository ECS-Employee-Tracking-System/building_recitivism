using Dapper;
using Microsoft.AspNetCore.Mvc;
using SeniorProjectECS.Library;
using SeniorProjectECS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Controllers
{
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateFilter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFilter(Filter Model)
        {
            String sql = BuildSQLFromFilter(Model);
            var parameters = BuildParamsFromFilter(Model);

            var returnModel = new StaffFilterViewModel();
            using (var con = DBHandler.GetSqlConnection())
            {
                var data = con.Query<StaffMember>(sql, parameters);
                returnModel.StaffMembers = data.ToList();
                returnModel.Filter = Model;
            }

            return RedirectToAction("Details", returnModel);
        }

        public ActionResult Details(StaffFilterViewModel model)
        {
            return View(model);
        }

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
            parameters = AddPropertyToExpando(parameters, "IsInactive", model.IsInactive);
            parameters = AddArrayToExpando(parameters, "CertName", model.CertCompleted);
            parameters = AddArrayToExpando(parameters, "PositionTitle", model.Position);
            parameters = AddArrayToExpando(parameters, "EducationLevel", model.EducationLevel);
            parameters = AddArrayToExpando(parameters, "EducationType", model.EducationType);
            parameters = AddArrayToExpando(parameters, "EducationDetail", model.EducationDetail);
            parameters = AddArrayToExpando(parameters, "CenterName", model.CenterName);
            parameters = AddArrayToExpando(parameters, "CenterCounty", model.CenterCounty);
            parameters = AddArrayToExpando(parameters, "CenterRegion", model.CenterRegion);

            return parameters;
        }

        private object AddArrayToExpando(ExpandoObject expando, String propertyName, List<String> list)
        {
            var expandoDic = expando as IDictionary<String, object>;

            for(int i=0; i<list.Count; i++)
            {
                expandoDic.Add(propertyName + i, list[i]);
            }

            return expandoDic;
        }

        private object AddPropertyToExpando(ExpandoObject expando, String propertyName, object item)
        {
            var expandoDic = expando as IDictionary<String, object>;
            expandoDic.Add(propertyName, item);

            return expandoDic;
        }

        private string BuildSQLFromFilter(Filter model)
        {
            String sql = "Select sm.StaffMemberID, sm.FirstName, sm.LastName, sm.Email,sm.DateofHire,sm.DirectorCredentials, sm.DCExpiration, sm.CDAInProgress, sm.CDAType, " +
                         "sm.CDAExpiration,sm.CDARenewalProcess,sm.Comments,sm.Goal,sm.MidYear,sm.EndYear,sm.GoalMet,sm.TAndAApp,sm.AppApp,sm.ClassCompleted,sm.ClassPaid, " +
                         "sm.RequiredHours,sm.HoursEarned,sm.Notes, sm.TermDate,sm.IsInactive, p.PositionID,p.PositionTitle, c.CenterID,c.Name,c.County,c.Region, e.EducationID,e.DegreeAbrv, " + 
                         "e.DegreeLevel, e.DegreeType, e.DegreeDetail, cc.CertCompletionDate, cert.CertificationID, cert.CertName, cert.CertExpireAmount " +
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
            //DateOfHire
            if(model.Goal) { sql += " AND (sm.Goal=@Goal)"; }
            if (model.MidYear) { sql += " AND (sm.MidYear=@MidYear)"; }
            if (model.EndYear) { sql += " AND (sm.EndYear=@EndYear)"; }
            if (model.GoalMet) { sql += " AND (sm.GoalMet=@GoalMet)"; }
            if (model.TAndAApp) { sql += " AND (sm.TAndAApp=@TAndAApp)"; }
            if (model.AppApp) { sql += " AND (sm.AppApp=@AppApp)"; }
            if (model.ClassCompleted) { sql += " AND (sm.ClassCompleted=@ClassCompleted)"; }
            if (model.ClassPaid) { sql += " AND (sm.ClassPaid=@ClassPaid)"; }
            //RequiredHours
            //HoursEarned
            //TermDate
            if (model.IsInactive) { sql += " AND (sm.IsInactive=@IsInactive)"; }
            sql = BuildSQLFromArray(sql, model.CertCompleted, "CertName", "cert");
            sql = BuildSQLFromArray(sql, model.Position, "PositionTitle", "p");
            sql = BuildSQLFromArray(sql, model.EducationLevel, "EducationLevel", "e");
            sql = BuildSQLFromArray(sql, model.EducationType, "EducationType", "e");
            sql = BuildSQLFromArray(sql, model.EducationDetail, "EducationDetail", "e");
            sql = BuildSQLFromArray(sql, model.CenterName, "CenterName", "c");
            sql = BuildSQLFromArray(sql, model.CenterCounty, "CenterCounty", "c");
            sql = BuildSQLFromArray(sql, model.CenterRegion, "CenterRegion", "c");
            //TimeUntilExpire
            //ShouldCheckPosition

            return sql;
        }

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
            using(var con = DBHandler.GetSqlConnection())
            {
                var filterList = con.Query("SELECT * FROM Filter");
                return Json(filterList);
            }
        }


        public IActionResult CDACompliance(int NumberOfDays = 90)
        {
            var con = DBHandler.GetSqlConnection();
            String sql = @"SELECT StaffMemberID, FirstName, LastName, Email, CDAExpiration FROM StaffMember 
                WHERE CDAExpiration is not NULL 
                and datediff(""dd"",CONVERT(date, getdate()),""CDAExpiration"") <=@NumberOfDays";

            
            var cdaexpiration = con.Query<StaffMember>(sql ,new{ NumberOfDays = NumberOfDays});
            return View(cdaexpiration);
        }//end View Index
    }
}

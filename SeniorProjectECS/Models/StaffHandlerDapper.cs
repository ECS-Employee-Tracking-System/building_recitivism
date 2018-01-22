using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProjectECS.Library;
using System.Data;
using System.Data.SqlClient;

namespace SeniorProjectECS.Models
{
    public class StaffHandlerDapper : IModelHandler<StaffMember>
    {
        /// <summary>
        /// Get a staff member based on their primary key
        /// </summary>
        /// <param name="id">The primary key of the staff member we want</param>
        /// <returns>A staff member</returns>
        public StaffMember GetModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var staffMembers = new Dictionary<int, StaffMember>();
                con.Query<StaffMember, Center, Education, StaffMember>("GetStaffMember", (staff, center, edu) =>
                {
                    if (edu != null && staffMembers.ContainsKey(staff.StaffMemberID))
                    {
                        staffMembers[staff.StaffMemberID].Education.Add(edu);
                    }
                    else
                    {
                        staff.Center = center;
                        if (edu != null)
                        {
                            staff.Education.Add(edu);
                        }
                        staffMembers.Add(staff.StaffMemberID, staff);
                    }
                    return staff;
                }, new { StaffMemberID = id }, splitOn: "CenterID,EducationID", commandType: CommandType.StoredProcedure);

                return staffMembers.Values.First();
            }//en using
        }//end GetModel()

        /// <summary>
        /// Get all staff members from the database
        /// </summary>
        /// <returns>All staff members from the database</returns>
        public IEnumerable<StaffMember> GetModels()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var staffMembers = new Dictionary<int, StaffMember>();
                con.Query<StaffMember, Center, Education, StaffMember>("GetStaffMember", (staff, center, edu) =>
                {
                    if (staffMembers.ContainsKey(staff.StaffMemberID))
                    {
                        staffMembers[staff.StaffMemberID].Education.Add(edu);
                    }
                    else
                    {
                        staff.Center = center;
                        staff.Education.Add(edu);
                        staffMembers.Add(staff.StaffMemberID, staff);
                    }
                    return staff;
                }, splitOn: "CenterID,EducationID", commandType: CommandType.StoredProcedure);
                return staffMembers.Values.ToList();
            }//end using
        }//end GetModels

        /// <summary>
        /// Add a new staff member to the database
        /// </summary>
        /// <param name="model">The data to passed in to the database</param>
        /// <returns>true if the transaction succeeds otherwise false</returns>
        public void AddModel(StaffMember model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Open();
                using (var transaction = con.BeginTransaction())
                {
                    model.StaffMemberID = con.Query<int>("AddNewStaffMember", BuildStaffMemberParams(model), transaction: transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    AddCenterToModel(model, con, transaction);
                    
                    foreach (Education edu in model.Education)
                    {
                        AddEducationToModel(model, edu, con, transaction);
                    }//end foreach education

                    transaction.Commit();
                }//end using transaction
            }//end using connection
        }//end AddModel()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void UpdateModel(StaffMember model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Open();
                using (var transaction = con.BeginTransaction())
                {
                    con.Query<int>("UpdateStaffMember", BuildStaffMemberParams(model), transaction: transaction, commandType: CommandType.StoredProcedure);

                    con.Execute("AddNewCenter", BuildCenterParams(model), transaction: transaction, commandType: CommandType.StoredProcedure);

                    transaction.Commit();
                }//end using transaction
            }//end using connection
        }//end updateModel()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteModel(int id)
        {
            var con = DBHandler.GetSqlConnection();
            con.Execute("deleteStaffMember", new { StaffMemberID = id }, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="con"></param>
        /// <param name="transaction"></param>
        public void AddCenterToModel(StaffMember model, SqlConnection con = null, SqlTransaction transaction = null)
        {
            if (con == null)
            {
                con = DBHandler.GetSqlConnection();
            }

            con.Execute("AddNewCenter", BuildCenterParams(model), transaction: transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="edu"></param>
        /// <param name="con"></param>
        /// <param name="transaction"></param>
        public void AddEducationToModel(StaffMember model, Education edu, SqlConnection con = null, SqlTransaction transaction = null)
        {
            if (con == null)
            {
                con = DBHandler.GetSqlConnection();
            }
            con.Execute("AddNewEducation", BuildEducationParams(model, edu), transaction: transaction, commandType: CommandType.StoredProcedure);
        }

        private object BuildStaffMemberParams(StaffMember model)
        {
            return new
            {
                StaffMemberID = model.StaffMemberID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfHire = model.DateOfHire,
                Position = model.Position,
                DirectorCredentials = model.DirectorCredentials,
                DCExpiration = model.DCExpiration,
                CDAInProgress = model.CDAInProgress,
                CDAType = model.CDAType,
                CDAExpiration = model.CDAExpiration,
                CDARenewalProcess = model.CDARenewalProcess,
                Comments = model.Comments,
                Goal = model.Goal,
                MidYear = model.MidYear,
                EndYear = model.EndYear,
                GoalMet = model.GoalMet,
                TAndAApp = model.TAndAApp,
                AppApp = model.AppApp,
                ClassCompleted = model.ClassCompleted,
                ClassPaid = model.ClassPaid,
                RequiredHours = model.RequiredHours,
                HoursEarned = model.HoursEarned,
                Notes = model.Notes,
                TermDate = model.TermDate,
                IsInactive = model.IsInactive
            };
        }

        private object BuildCenterParams(StaffMember model)
        {
            return new
            {
                StaffMemberID = model.StaffMemberID,
                CenterName = model.Center.Name,
                CenterCounty = model.Center.County,
                CenterRegion = model.Center.Region
            };
        }

        private object BuildEducationParams(StaffMember model, Education edu)
        {
            return new
            {
                StaffMemberID = model.StaffMemberID,
                DegreeAbrv = edu.DegreeAbrv,
                DegreeLevel = edu.DegreeLevel,
                DegreeType = edu.DegreeType,
                DegreeDetail = edu.DegreeDetail
            };
        }
    }//end StaffHandlerDapper
}//end namespace

using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProjectECS.Library;
using System.Data;

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
        /// <param name="Model">The data to passed in to the database</param>
        /// <returns>true if the transaction succeeds otherwise false</returns>
        public void AddModel(StaffMember Model)
        {
            //Extract just the staff member information
            var staffParams = new
            {
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Email = Model.Email,
                DateOfHire = Model.DateOfHire,
                Position = Model.Position,
                DirectorCredentials = Model.DirectorCredentials,
                DCExpiration = Model.DCExpiration,
                CDAInProgress = Model.CDAInProgress,
                CDAType = Model.CDAType,
                CDAExpiration = Model.CDAExpiration,
                CDARenewalProcess = Model.CDARenewalProcess,
                Comments = Model.Comments,
                Goal = Model.Goal,
                MidYear = Model.MidYear,
                EndYear = Model.EndYear,
                GoalMet = Model.GoalMet,
                TAndAApp = Model.TAndAApp,
                AppApp = Model.AppApp,
                ClassCompleted = Model.ClassCompleted,
                ClassPaid = Model.ClassPaid,
                RequiredHours = Model.RequiredHours,
                HoursEarned = Model.HoursEarned,
                Notes = Model.Notes,
                TermDate = Model.TermDate
            };

            using (var con = DBHandler.GetSqlConnection())
            {
                con.Open();
                using (var transaction = con.BeginTransaction())
                {
                    int id = con.Query<int>("AddNewStaffMember", staffParams, transaction: transaction, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    //Extract the center information
                    var centerParams = new
                    {
                        StaffMemberID = id,
                        CenterName = Model.Center.Name,
                        CenterCounty = Model.Center.County,
                        CenterRegion = Model.Center.Region
                    };

                    con.Execute("AddNewCenter", centerParams, transaction: transaction, commandType: CommandType.StoredProcedure);

                    //Extract the eduction information and execute for each one

                    foreach(Education edu in Model.Education)
                    {
                        var educationParams = new
                        {
                            StaffMemberID = id,
                            DegreeAbrv = edu.DegreeAbrv,
                            DegreeLevel = edu.DegreeLevel,
                            DegreeType = edu.DegreeType,
                            DegreeDetail = edu.DegreeDetail
                        };

                        con.Execute("AddNewEducation", educationParams, transaction: transaction, commandType: CommandType.StoredProcedure);
                    }//end foreach education
                    transaction.Commit();
                }//end using transaction
            }//end using connection
        }//end AddModel()
    }//end StaffHandlerDapper
}//end namespace

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
                StaffMember staffMember = null;
                con.Query<StaffMember, Position, Center, Education, DateTime?, Certification, StaffMember>("GetStaffMember", (staff, pos, center, edu, certCompleted, cert) =>
                {
                    if(staffMember == null)
                    {
                        staffMember = staff;
                    }

                    if(pos != null && !staffMember.Positions.Any(p => p.PositionID == pos.PositionID))
                    {
                        staffMember.Positions.Add(pos);
                    }

                    if(edu != null && !staffMember.Education.Any(e => e.EducationID == edu.EducationID))
                    {
                        staffMember.Education.Add(edu);
                    }

                    if(cert != null && !staffMember.CompletedCerts.Any(cc => cc.Cert.CertificationID == cert.CertificationID))
                    {
                        var newCertCompleted = new CertCompletion
                        {
                            Cert = cert,
                            DateCompleted = certCompleted
                        };
                        staffMember.CompletedCerts.Add(newCertCompleted);
                    }

                    staffMember.Center = center;

                    return staff;
                }, new { StaffMemberID = id }, splitOn: "PositionID,CenterID,EducationID,CertCompletionDate,CertificationID", commandType: CommandType.StoredProcedure);

                return staffMember;
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
                var staffMembers = new List<StaffMember>();
                con.Query<StaffMember, Position, Center, Education, StaffMember>("GetStaffMember", (staff, pos, center, edu) =>
                {
                    int foundStaff = staffMembers.FindIndex(s => s.StaffMemberID == staff.StaffMemberID);
                    if (foundStaff == -1)
                    {
                        if (pos != null) { staff.Positions.Add(pos); }
                        if (center != null) { staff.Center = center; }
                        if (edu != null) { staff.Education.Add(edu); }
                        staffMembers.Add(staff);
                    } else {
                        if(pos != null && !staffMembers[foundStaff].Positions.Any(p => p.PositionID == pos.PositionID))
                        {
                            staffMembers[foundStaff].Positions.Add(pos);
                        }

                        if(edu != null && !staffMembers[foundStaff].Education.Any(e => e.EducationID == edu.EducationID))
                        {
                            staffMembers[foundStaff].Education.Add(edu);
                        }
                    }
                    return staff;
                }, splitOn: "PositionID,CenterID,EducationID", commandType: CommandType.StoredProcedure);
                return staffMembers;
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

                    foreach(Position pos in model.Positions)
                    {
                        AddPositionsToModel(model.StaffMemberID, pos.PositionTitle, con, transaction);
                    }

                    AddCenterToModel(model, con, transaction);

                    //checks to make sure a null entry is entered for education, asumes that every entry will have a degree level
                    if (model.Education[0].DegreeLevel != null)
                    {
                        foreach (Education edu in model.Education)
                        {
                            AddEducationToModel(model.StaffMemberID, edu, con, transaction);
                        }//end foreach education
                    }//end the if check for nulls
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

        public void AddPositionsToModel(int StaffMemberID, String PositionTitle, SqlConnection con = null, SqlTransaction transaction = null)
        {
            if (con == null)
            {
                con = DBHandler.GetSqlConnection();
            }

            var output = con.Execute("AddNewPosition", new { StaffMemberID = StaffMemberID, PositionTitle = PositionTitle }, transaction: transaction, commandType: CommandType.StoredProcedure);
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
        /// Add an education to the model
        /// </summary>
        /// <param name="staffMemberID">The id of the staff member to add to</param>
        /// <param name="edu">The education object to add</param>
        /// <param name="con">The sql connection if any</param>
        /// <param name="transaction">The transaction if any</param>
        public void AddEducationToModel(int staffMemberID, Education edu, SqlConnection con = null, SqlTransaction transaction = null)
        {
            if (con == null)
            {
                con = DBHandler.GetSqlConnection();
            }
            con.Execute("AddNewEducation", BuildEducationParams(staffMemberID, edu), transaction: transaction, commandType: CommandType.StoredProcedure);
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

        private object BuildEducationParams(int staffMemberID, Education edu)
        {
            return new
            {
                StaffMemberID = staffMemberID,
                DegreeAbrv = edu.DegreeAbrv,
                DegreeLevel = edu.DegreeLevel,
                DegreeType = edu.DegreeType,
                DegreeDetail = edu.DegreeDetail
            };
        }
    }//end StaffHandlerDapper
}//end namespace

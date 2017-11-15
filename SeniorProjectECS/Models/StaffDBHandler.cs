
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SeniorProjectECS.Library;
using Dapper;
using System.Linq;
namespace SeniorProjectECS.Models
{
    public class StaffDBHandler
    {
        // ********** VIEW Movie DETAILS ********************
        public List<StaffMember> GetStaffMember()
        {

            var con = DBHandler.GetSqlConnection();
            List<StaffMember> Stafflist = new List<StaffMember>();

            SqlCommand cmd = new SqlCommand("GetStaffMemberDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {

                Stafflist.Add(
                    new StaffMember
                    {
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        DateOfHire = Convert.ToDateTime(dr["DateOfHire"] as DateTime?),
                        Position = Convert.ToString(dr["Position"]),
                        CenterID = Convert.ToInt32(dr["CenterID"]),
                        DirectorCredentials = Convert.ToBoolean(dr["DirectorCredentials"] as int?),
                        DCExpiration = Convert.ToDateTime(dr["DCExpiration"] as DateTime?),
                        CDAInProgress = Convert.ToBoolean(dr["CDAInProgress"] as int?),
                        CDAType = Convert.ToString(dr["CDAType"]),
                        CDAExpiration = Convert.ToDateTime(dr["CDAExpiration"] as DateTime ?),
                        CDARenewalProcess = Convert.ToString(dr["CDARenewalProcess"]),
                        Comments = Convert.ToString(dr["Comments"]),
                        Goal = Convert.ToString(dr["Goal"]),
                        MidYear = Convert.ToString(dr["MidYear"]),
                        EndYear = Convert.ToString(dr["EndYear"]),
                        GoalMet = Convert.ToBoolean(dr["GoalMet"] as int?),
                        TAndAApp = Convert.ToString(dr["TAndAApp"]),
                        AppApp = Convert.ToString(dr["AppApp"]),
                        ClassCompleted = Convert.ToString(dr["ClassCompleted"]),
                        ClassPaid = Convert.ToBoolean(dr["ClassPaid"] as int?),
                        RequiredHours = Convert.ToInt32(dr["RequiredHours"] as int?),
                        HoursEarned = Convert.ToInt32(dr["HoursEarned"] as int?),
                        Notes = Convert.ToString(dr["Notes"]),
                        TermDate = Convert.ToString(dr["TermDate"]),
                        CenterName = Convert.ToString(dr["CenterName"]),
                        CenterCounty = Convert.ToString(dr["CenterCounty"]),
                        CenterRegion= Convert.ToString(dr["CenterRegion"]),
                        DegreeAbrv = Convert.ToString(dr["DegreeAbrv"]),
                        DegreeDetail = Convert.ToString(dr["DegreeDetail"]),
                        DegreeLevel = Convert.ToString(dr["DegreeLevel"]),
                        DegreeType = Convert.ToString(dr["DegreeType"])
                    });
            }
            return Stafflist;
        }
        public bool AddStaffMember(StaffMember smodel)
        {
            var con = DBHandler.GetSqlConnection();
            SqlCommand cmd = new SqlCommand("AddNewStaffMember", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CenterName", smodel.CenterName);
            cmd.Parameters.AddWithValue("@CenterCounty", smodel.CenterCounty);
            cmd.Parameters.AddWithValue("@CenterRegion", smodel.CenterRegion);
            cmd.Parameters.AddWithValue("@DegreeAbrv", smodel.DegreeAbrv);
            cmd.Parameters.AddWithValue("DegreeLevel", smodel.DegreeLevel);
            cmd.Parameters.AddWithValue("@DegreeType", smodel.DegreeType);
            cmd.Parameters.AddWithValue("@DegreeDetail", smodel.DegreeDetail);
            cmd.Parameters.AddWithValue("@FirstName", smodel.FirstName);
            cmd.Parameters.AddWithValue("@LastName", smodel.LastName);
            cmd.Parameters.AddWithValue("@Email", smodel.Email);
            cmd.Parameters.AddWithValue("@DateOfHire", smodel.DateOfHire);
            cmd.Parameters.AddWithValue("@Position", smodel.Position);
            cmd.Parameters.AddWithValue("@DirectorCredentials", smodel.DirectorCredentials);
            cmd.Parameters.AddWithValue("@DCExpiration", smodel.DCExpiration);
            cmd.Parameters.AddWithValue("@CDAInProgress", smodel.CDAInProgress);
            cmd.Parameters.AddWithValue("@CDAType", smodel.CDAType);
            cmd.Parameters.AddWithValue("@CDAExpiration", smodel.CDAExpiration);
            cmd.Parameters.AddWithValue("@CDARenewalProcess", smodel.CDARenewalProcess);
            cmd.Parameters.AddWithValue("@Comments", smodel.Comments);
            cmd.Parameters.AddWithValue("@Goal", smodel.Goal);
            cmd.Parameters.AddWithValue("@MidYear", smodel.MidYear);
            cmd.Parameters.AddWithValue("@EndYear", smodel.EndYear);
            cmd.Parameters.AddWithValue("@GoalMet", smodel.GoalMet);
            cmd.Parameters.AddWithValue("@TAndAApp", smodel.TAndAApp);
            cmd.Parameters.AddWithValue("@AppApp", smodel.AppApp);
            cmd.Parameters.AddWithValue("@ClassCompleted", smodel.ClassCompleted);
            cmd.Parameters.AddWithValue("@ClassPaid", smodel.ClassPaid);
            cmd.Parameters.AddWithValue("@RequiredHours", smodel.RequiredHours);
            cmd.Parameters.AddWithValue("@HoursEarned", smodel.HoursEarned);
            cmd.Parameters.AddWithValue("@Notes", smodel.Notes);
            cmd.Parameters.AddWithValue("@TermDate", smodel.TermDate);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<StaffMember> GetAllStaff()
        {
            try
            {
                var con = DBHandler.GetSqlConnection();
                con.Open();
                IList<StaffMember> StaffList = SqlMapper.Query<StaffMember>(
                                  con, "GetStaffMemberDetails").ToList();
                con.Close();
                return StaffList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    
}

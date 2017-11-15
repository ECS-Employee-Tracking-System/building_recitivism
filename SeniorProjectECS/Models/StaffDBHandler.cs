
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
        private SqlConnection con;
        private void Connection()
        {
            var con = DBHandler.GetSqlConnection();
        }
        // ********** VIEW Movie DETAILS ********************
        public List<StaffMember> GetStaffMember()
        {
            Connection();
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
                        DateOfHire = Convert.ToDateTime(dr["DateOfHire"]),
                        Position = Convert.ToString(dr["Position"]),
                        CenterID = Convert.ToInt32(dr["CenterID"]),
                        DirectorCredentials = Convert.ToBoolean(dr["DirectorCredentials"]),
                        DCExpiration = Convert.ToDateTime(dr["DCExpiration"]),
                        CDAInProgress = Convert.ToBoolean(dr["CDAInProgress"]),
                        CDAType = Convert.ToString(dr["CDAType"]),
                        CDAExpiration = Convert.ToDateTime(dr["CDAExpiration"]),
                        CDARenewalProcess = Convert.ToString(dr["CDARenewalProcess"]),
                        Comments = Convert.ToString(dr["Comments"]),
                        Goal = Convert.ToString(dr["Goal"]),
                        MidYear = Convert.ToString(dr["MidYear"]),
                        EndYear = Convert.ToString(dr["EndYear"]),
                        GoalMet = Convert.ToBoolean(dr["GoalMet"]),
                        TAndAApp = Convert.ToString(dr["TAndAApp"]),
                        AppApp = Convert.ToString(dr["AppApp"]),
                        ClassCompleted = Convert.ToString(dr["ClassCompleted"]),
                        ClassPaid = Convert.ToBoolean(dr["ClassPaid"]),
                        RequiredHours = Convert.ToInt32(dr["RequiredHours"]),
                        HoursEarned = Convert.ToInt32(dr["HoursEarned"]),
                        Notes = Convert.ToString(dr["Notes"]),
                        TermDate = Convert.ToString(dr["TermDate"]),

                    });
            }
            return Stafflist;
        }

        public List<StaffMember> GetAllStaff()
        {
            try
            {
                Connection();
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

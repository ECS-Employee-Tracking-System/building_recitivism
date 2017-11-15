using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;


namespace SeniorProjectECS.Models
{
    public class StaffMember
    {
        // basic information
        public int StaffMemberID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime DateOfHire { get; set; }
        public String Position { get; set; }
        public int CenterID { get; set; }
        public Boolean DirectorCredentials { get; set; }
        public DateTime DCExpiration { get; set; }
        public Boolean CDAInProgress { get; set; }
        public String CDAType { get; set; }
        public DateTime CDAExpiration { get; set; }
        public String CDARenewalProcess { get; set; }
        public String Comments { get; set; }
        public String Goal { get; set; }
        public String MidYear { get; set; }
        public String EndYear { get; set; }
        public Boolean GoalMet { get; set; }
        public String TAndAApp { get; set; }
        public String AppApp { get; set; }
        public String ClassCompleted { get; set; }
        public Boolean ClassPaid { get; set; }
        public int RequiredHours { get; set; }
        public int HoursEarned { get; set; }
        public String Notes{ get; set; }
        public String TermDate { get; set; }

        public Center Center { get; set; }
        public IEnumerable<Education> Education { get; set; }

        /// <summary>
        /// This function gets a staff member and build all object nessisary for it.
        /// </summary>
        /// <param name="id">The ID of the staff member that we want to get.</param>
        /// <returns>A staff member</returns>
        public static StaffMember GetStaffMember(int? id)
        {
            if (id == null) { return null; }
            var con = DBHandler.GetSqlConnection();

            String sql = "SELECT * FROM StaffMember WHERE StaffMemberID=" + id;
            var staffMember = con.Query<StaffMember>(sql).First();

            BuildStaffMember(staffMember, con);

            return staffMember;
        }// end GetStaffMember

        /// <summary>
        /// This function gets all staff members that fall within certain criteria.
        /// </summary>
        /// <param name="where">The WHERE statement, leave blank to get everything</param>
        /// <returns>An IEnumerable contianing a list of StaffMembers</returns>
        public static IEnumerable<StaffMember> GetStaffMembers(String where)
        {
            var con = DBHandler.GetSqlConnection();
            String sql;

            // check if we want to use the WHERE statement
            if (where.Length > 0)
            {
               sql = "SELECT * FROM StaffMember WHERE " + where;
            } else {
                sql = "SELECT * FROM StaffMember";
            }//end length check
            
            var staffMembers = con.Query<StaffMember>(sql);

            // add data from other tables
            foreach(var staff in staffMembers)
            {
                BuildStaffMember(staff, con);
            }//end data

            return staffMembers;
        }//end GetSTaffMembers

        /// <summary>
        /// Create the rest of a staff member.
        /// </summary>
        /// <param name="staff">The staff member to finish building</param>
        /// <param name="con">The SQLConnection</param>
        private static void BuildStaffMember(StaffMember staff, SqlConnection con)
        {
            // add the center
            String sql = "SELECT * FROM Center WHERE CenterID=" + staff.CenterID;
            var center = con.Query<Center>(sql).First();
            staff.Center = center;

            // add the education
            sql = "SELECT * FROM Education WHERE StaffMemberID=" + staff.StaffMemberID;
            staff.Education = con.Query<Education>(sql);
            foreach (var edu in staff.Education)
            {
                edu.StaffMember = staff;
            }
        }//end BuildStaffMember
        
    }//end class StaffMember
}//end namespace SeniorProjecECS

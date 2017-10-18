using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
        public Center Center { get; set; }

        public String CDAType { get; set; }
        public DateTime CDAExpiration { get; set; }

        public IEnumerable<Education> Education { get; set; }

        public static StaffMember GetStaffMember(int? id)
        {
            if (id == null) { return null; }
            var con = DBHandler.GetSqlConnection();

            String sql = "SELECT * FROM StaffMember WHERE StaffMemberID=" + id;
            var staffMember = con.Query<StaffMember>(sql).First();

            BuildStaffMember(staffMember, con);

            return staffMember;
        }

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
    }
}

using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace SeniorProjectECS.Models
{
    public class Center
    {
        public int CenterID { get; set; }
        public String Name { get; set; }
        public String County { get; set; }
        public String Region { get; set; }

        public IEnumerable<StaffMember> Staff { get; set; }

        /// <summary>
        /// This function gets a center and builds all objects neccissary for it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Center GetCenter(int? id)
        {
            if(id == null) { return null; }
            var con = DBHandler.GetSqlConnection();
            String sql = "SELECT * FROM Center WHERE CenterID=" + id;
            var center = con.Query<Center>(sql).First();

            BuildCenter(center, con);
            
            return center;
        }

        /// <summary>
        /// This function gets all centers
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Center> GetCenters()
        {
            var con = DBHandler.GetSqlConnection();
            String sql = "SELECT * FROM Center";
            var centers = con.Query<Center>(sql);

            return centers;
        }

        private static void BuildCenter(Center center, SqlConnection con)
        {
            // add all staff members
            center.Staff = StaffMember.GetStaffMembers("CenterID=" + center.CenterID);
        }
    }
}
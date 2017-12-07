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
            var staffMembers = new Dictionary<int, StaffMember>();
            var con = DBHandler.GetSqlConnection();
            con.Query<StaffMember, Center, Education, StaffMember>("GetStaffMember", (staff, center, edu) => 
            {
                if (staffMembers.ContainsKey(staff.StaffMemberID))
                {
                    staffMembers[staff.StaffMemberID].Education.Add(edu);
                } else {
                    staff.Center = center;
                    staff.Education.Add(edu);
                    staffMembers.Add(staff.StaffMemberID, staff);
                }
                return staff;
            }, new { StaffMemberID = id }, splitOn: "CenterID,EducationID", commandType: CommandType.StoredProcedure);

            return staffMembers.Values.First();
        }

        /// <summary>
        /// Get all staff members from the database
        /// </summary>
        /// <returns>All staff members from the database</returns>
        public IEnumerable<StaffMember> GetModels()
        {
            var con = DBHandler.GetSqlConnection();

            var staffMembers = new Dictionary<int, StaffMember>();
            con.Query<StaffMember, Center, Education, StaffMember>("GetStaffMember", (staff, center, edu) =>
            {
                if(staffMembers.ContainsKey(staff.StaffMemberID))
                {
                    staffMembers[staff.StaffMemberID].Education.Add(edu);
                } else {
                    staff.Center = center;
                    staff.Education.Add(edu);
                    staffMembers.Add(staff.StaffMemberID, staff);
                }
                return staff;
            }, splitOn: "CenterID,EducationID", commandType: CommandType.StoredProcedure);
            return staffMembers.Values.ToList();
        }
    }
}

using SeniorProjectECS.Library;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SeniorProjectECS.Models
{
    public class CenterHandlerDapper : IModelHandler<Center>
    {
        public Center GetModel(int id)
        {
            var con = DBHandler.GetSqlConnection();
            var centers = new Dictionary<int, Center>();
            con.Query<Center, StaffMember, Center>("GetCenterSingle", (center, staff) =>
            {
                if(centers.ContainsKey(center.CenterID))
                {
                    centers[center.CenterID].Staff.Add(staff);
                } else {
                    center.Staff.Add(staff);
                    centers.Add(center.CenterID, center);
                }
                return center;
            }, new { CenterID = id }, splitOn: "StaffMemberID", commandType: CommandType.StoredProcedure);

            return centers.Values.First();
        }

        public IEnumerable<Center> GetModels()
        {
            throw new NotImplementedException();
        }
    }
}

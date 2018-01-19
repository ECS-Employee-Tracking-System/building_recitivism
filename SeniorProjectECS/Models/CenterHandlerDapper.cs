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
            con.Query<Center, StaffMember, Center>("GetCenter", (center, staffMember) =>
            {
                if(centers.ContainsKey(center.CenterID))
                {
                    centers[center.CenterID].Staff.Add(staffMember);
                } else {
                    center.Staff.Add(staffMember);
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

        public void AddModel(Center Model)
        {

        }

        public void UpdateModel(Center Model)
        {

        }
    }
}

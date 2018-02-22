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
            con.Query<Center, StaffMember, Position, Center>("GetCenter", (center, staffMember, position) =>
            {
                if(position != null)
                {
                    staffMember.Positions.Add(position);
                }
                if(centers.ContainsKey(center.CenterID))
                {
                    centers[center.CenterID].Staff.Add(staffMember);
                } else {
                    center.Staff.Add(staffMember);
                    centers.Add(center.CenterID, center);
                }
                return center;
            }, new { CenterID = id }, splitOn: "StaffMemberID,PositionID", commandType: CommandType.StoredProcedure);

            if (centers.Count == 0)
            {
                String sql = "SELECT * FROM Center WHERE CenterID = @id";
                var simpleCenter = con.Query<Center>(sql, new { id = id });
                return simpleCenter.FirstOrDefault();
            } else {
                return centers.Values.First(); ;
            }
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

        public void DeleteModel(int id)
        {

        }
    }
}

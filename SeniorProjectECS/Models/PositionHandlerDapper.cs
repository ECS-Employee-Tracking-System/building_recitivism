using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class PositionHandlerDapper : IModelHandler<Position>
    {
        public void AddModel(Position Model)
        {
            throw new NotImplementedException();
        }

        public void DeleteModel(int id)
        {
            throw new NotImplementedException();
        }

        public Position GetModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "SELECT * FROM Position as p" +
                    "Left Outer Join PositionReq as pr on pr.PositionID=p.PositionID" +
                    "Left Outer Join Certification as c on c.CertificationID=pr.CertificationID" +
                    "WHERE PositionID=@PosID";

                return null;
            }
        }

        public IEnumerable<Position> GetModels()
        {
            using(var con = DBHandler.GetSqlConnection())
            {
                var data = con.Query<Position>("SELECT * FROM Position");
                return data;
            }
        }

        public void UpdateModel(Position Model)
        {
            throw new NotImplementedException();
        }
    }
}

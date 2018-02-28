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
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "INSERT INTO Position (PositionTitle) VALUES (@PosTitle)";
                con.Execute(sql, new { PosTitle = Model.PositionTitle });
            }
        }

        public void DeleteModel(int id)
        {
            throw new NotImplementedException();
        }

        public Position GetModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "SELECT p.PositionID, p.PositionTitle, c.CertificationID, c.CertName, c.CertExpireAmount " +
                             "FROM Position as p " +
                             "Left Outer Join PositionReq as pr on pr.PositionID = p.PositionID " +
                             "Left Outer Join Certification as c on c.CertificationID = pr.CertificationID " +
                             "WHERE p.PositionID=@PosID";

                Position position = null;
                con.Query<Position, Certification, Position>(sql, (pos, cert) =>
                {
                    if(position == null)
                    {
                        position = pos;
                    }

                    if(cert != null && !position.RequiredCerts.Any(c => c.CertificationID == cert.CertificationID))
                    {
                        position.RequiredCerts.Add(cert);
                    }

                    return pos;
                }, new { PosID = id }, splitOn: "PositionID, CertificationID");

                return position;
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
            using(var con = DBHandler.GetSqlConnection())
            {
                String sql = "UPDATE Position SET PositionTitle=@PosTitle WHERE PositionID=@PosID";
                con.Execute(sql, new { PosTitle = Model.PositionTitle, PosID = Model.PositionID });
            }
        }
    }
}

using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class CertHandlerDapper : IModelHandler<Certification>
    {
        public void AddModel(Certification Model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "INSERT INTO Certification (CertName, CertExpireAmount) VALUES (@CertName, @CertExpire)";
                con.Execute(sql, new { CertName = Model.CertName, CertExpire = Model.CertExpireAmount });
            }
        }

        public void DeleteModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "DELETE FROM Certification WHERE CertificationID=@CertID";
                con.Execute(sql, new { CertID = id });
            }
        }

        public Certification GetModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var data = con.Query<Certification>("SELECT * FROM Certification WHERE CertificationID=@CertID", new { CertID = id });

                return data.FirstOrDefault();
            }
        }

        public IEnumerable<Certification> GetModels()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                var data = con.Query<Certification>("SELECT * FROM Certification");

                return data;
            }
        }

        public void UpdateModel(Certification Model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "UPDATE Certification SET CertName=@CertName, CertExpireAmount=@CertExpire WHERE CertificationID=@CertID";
                con.Execute(sql, new { CertName = Model.CertName, CertExpire = Model.CertExpireAmount, CertID = Model.CertificationID });
            }
        }
    }
}

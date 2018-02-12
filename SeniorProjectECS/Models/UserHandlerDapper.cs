using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace SeniorProjectECS.Models
{
    public class UserHandlerDapper : IModelHandler<User>
    {
        public void AddModel(User Model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query("AddNewUser", Model, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query("DeleteUser", new { UserID = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public User GetModel(int id)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                return con.Query<User>("SELECT * FROM ECSUser WHERE UserID = @id", new { id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<User> GetModels()
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                return con.Query<User>("SELECT * FROM ECSUser");
            }
        }

        public void UpdateModel(User Model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                con.Query("UpdateUser", Model, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SeniorProjectECS.Models
{
    public class FilterHandlerDapper : IModelHandler<Filter>
    {
        public void AddModel(Filter Model)
        {
            using (var con = DBHandler.GetSqlConnection())
            {
                String sql = "INSERT INTO Filter (FirstName, LastName, Email, DateOfHire, " +
                    "Goal, MidYear, EndYear, GoalMet, TAndAApp, AppApp, ClassCompleted, ClassPaid, " +
                    "RequiredHours, HoursEarned, TermDate, IsInactive, CertCompleted, Position, " +
                    "EducationLevel, EducationType, EducationDetail, CenterName, CenterCounty, CenterRegion, " +
                    "TimeUntilExpire, ShouldCheckPositionReq) VALUES (@FirstName, @LastName, @Email, " +
                    "@DateOfHire, @Goal, @MidYear, @EndYear, @GoalMet, @TAndAApp, @AppApp, @ClassCompleted, " +
                    "@ClassPaid, @RequiredHours, @HoursEarned, @TermDate, @IsInactive, @CertCompleted, " +
                    "@Position, @EducationLevel, @EducationType, @EducationDetail, @CenterName, @CenterCounty, " +
                    "@CenterRegion, @TimeUntilExpire, @ShouldCheckPositionReq)";

                con.Execute(sql, BuildFilterParams(Model));
            }
        }

        public void DeleteModel(int id)
        {
            using(var con = DBHandler.GetSqlConnection())
            {
                String sql = "DELETE FROM Filter WHERE FilterID=@FilterID";
                con.Execute(sql, new { FilterID = id });
            }
        }

        public Filter GetModel(int id)
        {
            using(var con = DBHandler.GetSqlConnection())
            {
                String sql = "SELECT * FROM Filter WHERE FilterID=@FilterID";
                var data = con.Query<Filter>(sql, new { FilterID = id });

                return data.FirstOrDefault();
            }
        }

        public IEnumerable<Filter> GetModels()
        {
            throw new NotImplementedException();
        }

        public void UpdateModel(Filter Model)
        {
            throw new NotImplementedException();
        }

        private object BuildFilterParams(Filter Model)
        {
            return new
            {
                FirstName = String.Join(",", Model.FirstName),
                LastName = String.Join(",", Model.LastName),
                Email = String.Join(",", Model.Email),
                DateOfHire = Model.BeginDateOfHire,
                Goal = Model.Goal,
                MidYear = Model.MidYear,
                EndYear = Model.EndYear,
                GoalMet = Model.GoalMet,
                TAndAApp = Model.TAndAApp,
                AppApp = Model.AppApp,
                ClassCompleted = Model.ClassCompleted,
                ClassPaid = Model.ClassPaid,
                RequiredHours = Model.RequiredHours,
                HoursEarned = Model.HoursEarned,
                TermDate = Model.BeginTermDate,
                IsInactive = Model.IsInactive,
                CertCompleted = String.Join(",", Model.CertCompleted),
                Position = String.Join(",", Model.Position),
                EducationLevel = String.Join(",", Model.EducationLevel),
                EducationType = String.Join(",", Model.EducationType),
                EducationDetail = String.Join(",", Model.EducationDetail),
                CenterName = String.Join(",", Model.CenterName),
                CenterCounty = String.Join(",", Model.CenterCounty),
                CenterRegion = String.Join(",", Model.CenterRegion),
                TimeUntilExpire = Model.TimeUntilExpire,
                ShouldCheckPositionReq = Model.ShouldCheckPositionReq
            };
        }
    }
}

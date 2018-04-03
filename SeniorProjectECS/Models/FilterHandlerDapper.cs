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
                String sql = "INSERT INTO Filter (FirstName, LastName, Email, BeginDateOfHire, EndDateOfHire, " +
                    "Goal, MidYear, EndYear, GoalMet, TAndAApp, AppApp, ClassCompleted, ClassPaid, " +
                    "RequiredHours, HoursEarned, BeginTermDate, EndTermDate, IsInactive, CertCompleted, Position, " +
                    "EducationLevel, EducationType, EducationDetail, CenterName, CenterCounty, CenterRegion, " +
                    "TimeUntilExpire, ShouldCheckPositionReq) VALUES (@FirstName, @LastName, @Email, " +
                    "@BeginDateOfHire, @EndDateOfHire, @Goal, @MidYear, @EndYear, @GoalMet, @TAndAApp, @AppApp, @ClassCompleted, " +
                    "@ClassPaid, @RequiredHours, @HoursEarned, @BeginTermDate, @EndTermDate, @IsInactive, @CertCompleted, " +
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

        public void UpdateModel(Filter model)
        {
            String sql = "UPDATE Filter SET FirstName = @FirstName, LastName = @LastName, Email = @Email, BeginDateOfHire = @BeginDateOfHire, EndDateOfHire = @EndDateOfHire" +
                    "Goal = @Goal, MidYear = @MidYear, EndYear = @EndYear, GoalMet = @GoalMet, TAndAApp = @TAndAApp, AppApp = @AppApp, " +
                    "ClassCompleted = @ClassCompleted, ClassPaid = @ClassPaid, RequiredHours = @RequiredHours, HoursEarned = @HoursEarned, " +
                    "BeginTermDate = @BeginTermDate, EndTermDate = @EndTermDate, IsInactive = @IsInactive, CertCompleted = @CertCompleted, Position = @Position, EducationLevel = @EducationLevel, " +
                    "EducationType = @EducationType, EducationDetail = @EducationDetail, Name = @CenterName, County = @CenterCounty, " +
                    "Region = @CenterRegion, TimeUntilExpire = @TimeUntilExpire, ShouldCheckPositionReq = @ShouldCheckPositionReq WHERE FilterID=@FilterID";

            using(var con = DBHandler.GetSqlConnection())
            {
                con.Execute(sql, BuildFilterParams(model));
            }
        }

        private object BuildFilterParams(Filter Model)
        {
            return new
            {
                FirstName = String.Join(",", Model.FirstName),
                LastName = String.Join(",", Model.LastName),
                Email = String.Join(",", Model.Email),
                Model.BeginDateOfHire,
                Model.EndDateOfHire,
                Model.Goal,
                Model.MidYear,
                Model.EndYear,
                Model.GoalMet,
                Model.TAndAApp,
                Model.AppApp,
                Model.ClassCompleted,
                Model.ClassPaid,
                Model.RequiredHours,
                Model.HoursEarned,
                Model.BeginTermDate,
                Model.EndTermDate,
                Model.IsInactive,
                CertCompleted = String.Join(",", Model.CertCompleted),
                Position = String.Join(",", Model.Position),
                EducationLevel = String.Join(",", Model.EducationLevel),
                EducationType = String.Join(",", Model.EducationType),
                EducationDetail = String.Join(",", Model.EducationDetail),
                CenterName = String.Join(",", Model.CenterName),
                CenterCounty = String.Join(",", Model.CenterCounty),
                CenterRegion = String.Join(",", Model.CenterRegion),
                Model.TimeUntilExpire,
                Model.ShouldCheckPositionReq
            };
        }
    }
}

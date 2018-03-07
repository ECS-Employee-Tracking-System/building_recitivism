using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class Filter
    {
        public int FilterID { get; set; }

        public List<String> FirstName { get; set; }
        public List<String> LastName { get; set; }
        public List<String> Email { get; set; }
        public DateTime? DateOfHire { get; set; }
        public DateTime? BeginDateOfHire { get; set; }
        public DateTime? EndDateOfHire { get; set; }
        public bool? Goal { get; set; }
        public bool? MidYear { get; set; }
        public bool? EndYear { get; set; }
        public bool? GoalMet { get; set; }
        public bool? TAndAApp { get; set; }
        public bool? AppApp { get; set; }
        public bool? ClassCompleted { get; set; }
        public bool? ClassPaid { get; set; }
        public int? RequiredHours { get; set; }
        public int RequiredHoursType { get; set; }
        public int? HoursEarned { get; set; }
        public int HoursEarnedType { get; set; }
        public DateTime? BeginTermDate { get; set; }
        public DateTime? EndTermDate { get; set; }
        public bool IsInactive { get; set; }
        public List<String> CertCompleted { get; set; }
        public List<String> Position { get; set; }
        public List<String> EducationLevel { get; set; }
        public List<String> EducationType { get; set; }
        public List<String> EducationDetail { get; set; }
        public List<String> CenterName { get; set; }
        public List<String> CenterCounty { get; set; }
        public List<String> CenterRegion { get; set; }
        public int? TimeUntilExpire { get; set; }
        public bool ShouldCheckPositionReq { get; set; }
        public DateTime? TermDate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Filter()
        {
            FirstName = new List<String>();
            LastName = new List<String>();
            Email = new List<String>();
            CertCompleted = new List<String>();
            Position = new List<String>();
            EducationLevel = new List<String>();
            EducationType = new List<String>();
            EducationDetail = new List<String>();
            CenterName = new List<String>();
            CenterCounty = new List<String>();
            CenterRegion = new List<String>();
        }
    }
}

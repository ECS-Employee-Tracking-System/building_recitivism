using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class Filter
    {
        public int? FilterID { get; set; }

        [DisplayName("Filter Name")]
        public string FilterName { get; set; }

        [DisplayName("First Name")]
        public List<String> FirstName { get; set; }
        [DisplayName("Last Name")]
        public List<String> LastName { get; set; }
        public List<String> Email { get; set; }

        [DisplayName("Filter on date of hire after:")]
        [DataType(DataType.Date)]
        public DateTime? BeginDateOfHire { get; set; }
        [DisplayName("Filter on date of hire before:")]
        [DataType(DataType.Date)]
        public DateTime? EndDateOfHire { get; set; }

        [DisplayName("PDP Goal Begining of Year on File")]
        public bool? Goal { get; set; }
        [DisplayName("PDP Mid Year Goal on File")]
        public bool? MidYear { get; set; }
        [DisplayName("PDP End of Year Goal on File")]
        public bool? EndYear { get; set; }
        public bool? GoalMet { get; set; }
        [DisplayName("Tuition Assistance Application")]
        public bool? TAndAApp { get; set; }
        [DisplayName("Application Aproved")]
        public bool? AppApp { get; set; }
        [DisplayName("Class Completed")]
        public bool? ClassCompleted { get; set; }
        [DisplayName("Owes Money for Classes")]
        public bool? ClassPaid { get; set; }

        [DisplayName("Filter on ECS required training hours greater than:")]
        public int? BeginRequiredHours { get; set; }
        [DisplayName("Filter on ECS required training hours less than:")]
        public int? EndRequiredHours { get; set; }
        [DisplayName("Filter on current year hours earned greater than:")]
        public int? BeginHoursEarned { get; set; }
        [DisplayName("Filter on current year hours earned less than:")]
        public int? EndHoursEarned { get; set; }

        [DisplayName("Filter on term date after:")]
        [DataType(DataType.Date)]
        public DateTime? BeginTermDate { get; set; }
        [DisplayName("Filter on term date before:")]
        [DataType(DataType.Date)]
        public DateTime? EndTermDate { get; set; }

        [DisplayName("Inactive Staff")]
        public bool IsInactive { get; set; }
        [DisplayName("Completed certifications or training")]
        public List<String> CertCompleted { get; set; }
        [DisplayName("Position(s)")]
        public List<String> Position { get; set; }
        [DisplayName("Degree Level")]
        public List<String> EducationLevel { get; set; }
        [DisplayName("Degree Type")]
        public List<String> EducationType { get; set; }
        [DisplayName("Degree Detail")]
        public List<String> EducationDetail { get; set; }
        [DisplayName("Center Name")]
        public List<String> CenterName { get; set; }
        [DisplayName("Center County")]
        public List<String> CenterCounty { get; set; }
        [DisplayName("Center Region")]
        public List<String> CenterRegion { get; set; }

        [DisplayName("Days until certification expires")]
        public int? TimeUntilExpire { get; set; }
        public bool ShouldCheckPositionReq { get; set; }

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

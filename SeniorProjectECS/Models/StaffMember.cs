﻿using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectECS.Models
{
    public class CertCompletion
    {
        public Certification Cert { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateCompleted { get; set; }

        public int? DaysUntilExpire { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpireDate { get; set; }

        public bool IsRequired { get; set; }

        [DisplayName("Is Certification in Progress")]
        public bool CertInProgress { get; set; }
    }

    public class StaffMember
    {
        // basic information
        public int StaffMemberID { get; set; }

        [DisplayName("First Name")]
        [Required]
        public String FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public String LastName { get; set; }

        [EmailAddress]
        public String Email { get; set; }

        [DisplayName("Date of Hire")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? DateOfHire { get; set; }

        [DisplayName("Director Credentials")]
        public Boolean DirectorCredentials { get; set; }

        [DisplayName("Director Credentials Expiration")]
        [DataType(DataType.Date)]
        public DateTime? DCExpiration { get; set; }

        [DisplayName("Is CDA In Progress")]
        public Boolean CDAInProgress { get; set; }

        [DisplayName("CDA Type")]
        public String CDAType { get; set; }

        [DisplayName("CDA Expiration")]
        [DataType(DataType.Date)]
        public DateTime? CDAExpiration { get; set; }

        [DisplayName("CDA Renewal Process")]
        public String CDARenewalProcess { get; set; }

        public String Comments { get; set; }
        [DisplayName("PDP Goal Begining of Year on File")]
        public Boolean Goal { get; set; }
        [DisplayName("PDP Mid Year Goal on File")]
        public Boolean MidYear { get; set; }
        [DisplayName("PDP End of Year Goal on File")]
        public Boolean EndYear { get; set; }
        public Boolean GoalMet { get; set; }

        [DisplayName("Tuition Assistance Application")]
        public Boolean TAndAApp { get; set; }

        [DisplayName("Application Aproved")]
        public Boolean AppApp { get; set; }

        [DisplayName("Class Completed")]
        public Boolean ClassCompleted { get; set; }

        [DisplayName("Owes Money for Classes")]
        public Boolean ClassPaid { get; set; }

        [DisplayName("ECS Required Training Hours")]
        public int RequiredHours { get; set; }

        [DisplayName("Current Year Hours Earned")]
        public int HoursEarned { get; set; }

        public String Notes{ get; set; }

        [DisplayName("Term Date")]
        [DataType(DataType.Date)]
        public DateTime? TermDate { get; set; }

        [DisplayName("Deactivate Staff Member")]
        public bool IsInactive { get; set; }

        public Center Center { get; set; }
        public List<Education> Education { get; set; }
        public List<Position> Positions { get; set; }
        public List<CertCompletion> CompletedCerts { get; set; }

        public StaffMember()
        {
            Education = new List<Education>();
            Positions = new List<Position>();
            CompletedCerts = new List<CertCompletion>();
        }
    }//end class StaffMember
}//end namespace SeniorProjecECS

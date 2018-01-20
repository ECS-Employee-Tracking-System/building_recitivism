using Dapper;
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
    public class StaffMember
    {
        // basic information
        public int StaffMemberID { get; set; }

        [DisplayName("First Name")]
        public String FirstName { get; set; }

        [DisplayName("Last Name")]
        public String LastName { get; set; }
        public String Email { get; set; }

        [DisplayName("Date of Hire")]
        [DataType(DataType.Date)]
        public DateTime? DateOfHire { get; set; }
        public String Position { get; set; }

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
        public String Goal { get; set; }
        public String MidYear { get; set; }
        public String EndYear { get; set; }
        public Boolean GoalMet { get; set; }

        public String TAndAApp { get; set; }
        public String AppApp { get; set; }
        public String ClassCompleted { get; set; }

        [DisplayName("Owes Money for Classes")]
        public Boolean ClassPaid { get; set; }

        [DisplayName("Required Hours")]
        public int RequiredHours { get; set; }

        [DisplayName("Hours Earned")]
        public int HoursEarned { get; set; }

        public String Notes{ get; set; }

        [DisplayName("Term Date")]
        public String TermDate { get; set; }

        public Center Center { get; set; }
        public List<Education> Education { get; set; }

        [DisplayName("Deactivate Staff Member")]
        public bool IsInactive { get; set; }

        public StaffMember()
        {
            Education = new List<Education>();
        }
    }//end class StaffMember
}//end namespace SeniorProjecECS

using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;


namespace SeniorProjectECS.Models
{
    public class StaffMember
    {
        // basic information
        public int StaffMemberID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime DateOfHire { get; set; }
        public String Position { get; set; }
        public Boolean DirectorCredentials { get; set; }
        public DateTime DCExpiration { get; set; }
        public Boolean CDAInProgress { get; set; }
        public String CDAType { get; set; }
        public DateTime CDAExpiration { get; set; }
        public String CDARenewalProcess { get; set; }
        public String Comments { get; set; }
        public String Goal { get; set; }
        public String MidYear { get; set; }
        public String EndYear { get; set; }
        public Boolean GoalMet { get; set; }
        public String TAndAApp { get; set; }
        public String AppApp { get; set; }
        public String ClassCompleted { get; set; }
        public Boolean ClassPaid { get; set; }
        public int RequiredHours { get; set; }
        public int HoursEarned { get; set; }
        public String Notes{ get; set; }
        public String TermDate { get; set; }

        public Center Center { get; set; }
        public List<Education> Education { get; set; }

        public StaffMember()
        {
            Education = new List<Education>();
        }
    }//end class StaffMember
}//end namespace SeniorProjecECS

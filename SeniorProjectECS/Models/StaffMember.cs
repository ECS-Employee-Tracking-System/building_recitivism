using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProjectECS.Models
{
    public class StaffMember
    {
        public int StaffMemberID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public DateTime DateOfHire { get; set; }
        public String Position { get; set; }

        //Covers fields Region, County, and Center (name)
        public int CenterID { get; set; }
        public virtual Center Center { get; set; }

        //Possibly combine with others?
        public bool DirectorCredentials { get; set; }
        public DateTime DCExpiration { get; set; }

        //May want to combine these
        public bool CDAInProgress { get; set; }
        public String CDAType { get; set; } //Only 3 types could could create an enum for this one
        public DateTime CDAExpiration { get; set; }
        public String CDARenewalProcess { get; set; }

        public String Degree { get; set; }
        public String Comments { get; set; }

        //Need some clarification
        public String Goal { get; set; }
        public String MidYear { get; set; }
        public String EndYear { get; set; }
        public bool GoalMet { get; set; }
        public String TAndAApp { get; set; }
        public String AppApp { get; set; }
        public String ClassCompleted { get; set; }
        public bool ClassPaid { get; set; }
        public int RequiredHours { get; set; }
        public int HoursEarned { get; set; }
        public String Notes { get; set; }
        public String TermDate { get; set; }
    }
}
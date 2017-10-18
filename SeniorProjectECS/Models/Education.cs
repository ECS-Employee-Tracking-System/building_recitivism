using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class Education
    {
        public int EducationID { get; set; }
        public String DegreeAbrv { get; set; } // MS
        public String DegreeLevel { get; set; } // Masters
        public String DegreeType { get; set; } // Computing
        public String DegreeDetail { get; set; } // Master of Sciences in Computer Science

        public StaffMember StaffMember { get; set; }
    }
}

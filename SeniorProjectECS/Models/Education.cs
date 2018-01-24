using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class Education
    {
        public int EducationID { get; set; }

        [DisplayName("Degree Abbreviation")]
        public String DegreeAbrv { get; set; } // MS

        [DisplayName("Degree Level")]
        public String DegreeLevel { get; set; } // Masters

        [DisplayName("Degree Type")]
        public String DegreeType { get; set; } // Computing
         
        [DisplayName("Degree Detail")]
        public String DegreeDetail { get; set; } // Master of Sciences in Computer Science
    }
}

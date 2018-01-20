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

        [DisplayName("Degree Abbreviation ex. AA")]
        public String DegreeAbrv { get; set; } // MS

        [DisplayName("Degree Level ex. Masters")]
        public String DegreeLevel { get; set; } // Masters

        [DisplayName("Degree Type ex. Education")]
        public String DegreeType { get; set; } // Computing
         
        [DisplayName("Degree Detail ex. Early Childhood")]
        public String DegreeDetail { get; set; } // Master of Sciences in Computer Science
    }
}

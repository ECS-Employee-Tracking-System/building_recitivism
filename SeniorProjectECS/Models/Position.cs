using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class Position
    {
        public int PositionID { get; set; }

        [DisplayName("Position")]
        public String PositionTitle { get; set; }

        public List<Certification> RequiredCerts { get; set; }

        public Position()
        {
            RequiredCerts = new List<Certification>();
        }

        // todo add certification list
    }
}

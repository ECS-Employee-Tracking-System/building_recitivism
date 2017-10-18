using System;
using System.Collections.Generic;

namespace SeniorProjectECS.Models
{
    public class Center
    {
        public String Name { get; set; }
        public String County { get; set; }
        public String Region { get; set; }

        public IEnumerable<StaffMember> Staff { get; set; }
    }
}
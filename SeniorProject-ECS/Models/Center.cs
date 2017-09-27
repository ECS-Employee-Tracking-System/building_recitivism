using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeniorProject_ECS.Models
{
    public class Center
    {
        public int CenterID { get; set; }
        public String Name { get; set; }
        public String County { get; set; }
        public String Region { get; set; } 

        public virtual ICollection<StaffMember> StaffMembers { get; set; }
    }
}
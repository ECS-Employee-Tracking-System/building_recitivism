using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class StaffFilterViewModel
    {
        public List<StaffMember> StaffMembers { get; set; }
        public Filter Filter { get; set; }

        public StaffFilterViewModel()
        {
            StaffMembers = new List<StaffMember>();
        }
    }
}

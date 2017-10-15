using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class StaffMember
    {
        // basic information
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime DateOfHire { get; set; }
        public String Position { get; set; }

        public Center Center { get; set; }
    }
}

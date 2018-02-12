using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class User
    {
        public int UserID { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int AccessLevel { get; set; }
        public String PasswordHash { get; set; }
    }
}

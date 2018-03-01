using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String PasswordHash { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int AccessLevel { get; set; }
    }
}

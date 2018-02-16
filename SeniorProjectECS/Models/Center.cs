using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeniorProjectECS.Models
{
    public class Center
    {
        public int CenterID { get; set; }

        [DisplayName("Center Name")]
        [Required]
        public String Name { get; set; }

        [DisplayName("Center County")]
        [Required]
        public String County { get; set; }

        [DisplayName("Center Region")]
        [Required]
        public String Region { get; set; }

        public List<StaffMember> Staff { get; set; }

        public Center()
        {
            Staff = new List<StaffMember>();
        }
    }
}
using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.ComponentModel;

namespace SeniorProjectECS.Models
{
    public class Center
    {
        public int CenterID { get; set; }

        [DisplayName("Center Name")]
        public String Name { get; set; }

        [DisplayName("Center County")]
        public String County { get; set; }

        [DisplayName("Center Region")]
        public String Region { get; set; }

        public List<StaffMember> Staff { get; set; }

        public Center()
        {
            Staff = new List<StaffMember>();
        }
    }
}
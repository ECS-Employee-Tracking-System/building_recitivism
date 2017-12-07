using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace SeniorProjectECS.Models
{
    public class Center
    {
        public int CenterID { get; set; }
        public String Name { get; set; }
        public String County { get; set; }
        public String Region { get; set; }

        public List<StaffMember> Staff { get; set; }

        public Center()
        {
            Staff = new List<StaffMember>();
        }
    }
}
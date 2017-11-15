using Dapper;
using SeniorProjectECS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace SeniorProjectECS.Models
{
    public class CDACompliance
    {

        public int StaffMemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CDAExpiration { get; set; }
        public string Email { get; set; }
    }
}

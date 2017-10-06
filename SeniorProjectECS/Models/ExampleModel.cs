using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorProjectECS.Models
{
    public class ExampleModel
    {
        public int ExampleModelID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DoB { get; set; }
        public String DoesNotExistInDB { get; set; }
    }
}

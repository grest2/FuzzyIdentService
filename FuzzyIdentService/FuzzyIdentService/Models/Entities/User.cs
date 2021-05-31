using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public class User
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public int MiddleName { get; set; }
        public string LastName { get; set; }
    }
}

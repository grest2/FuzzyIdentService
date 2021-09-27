using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public abstract class BaseUser
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public class FoneticUser
    {
        public string id { get; set; }
        public string FoneticFirstName { get; set; }
        public string FoneticMiddleName { get; set; }
        public string FoneticLastName { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }
    }
}

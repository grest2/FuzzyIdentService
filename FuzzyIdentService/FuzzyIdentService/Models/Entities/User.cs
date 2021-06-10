using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public class User
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
        public FoneticUser fonUser { get; set; }

        public User(string id,string FirstName,string MiddleName,string LastName,string Index)
        {
            this.id = id;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Index = Index;
        }
    }
}

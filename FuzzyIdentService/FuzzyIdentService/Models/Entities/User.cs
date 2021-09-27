using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public class User : BaseUser
    {
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

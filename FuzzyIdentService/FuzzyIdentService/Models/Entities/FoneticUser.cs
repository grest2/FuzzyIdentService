using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public class FoneticUser : BaseUser
    {
        public string FoneticFirstName { get; set; }
        public string FoneticMiddleName { get; set; }
        public string FoneticLastName { get; set; }
        public string UserId { get; set; }
        public User user { get; set; }

        public FoneticUser(string id,string FoneticFirstName,string FoneticMiddleName,string FoneticLastName,string UserId)
        {
            this.id = id;
            this.FoneticFirstName = FoneticFirstName;
            this.FoneticMiddleName = FoneticMiddleName;
            this.FoneticLastName = FoneticLastName;
            this.UserId = UserId;
            this.FirstName = FoneticFirstName;
            this.MiddleName = FoneticMiddleName;
        }
    }
}

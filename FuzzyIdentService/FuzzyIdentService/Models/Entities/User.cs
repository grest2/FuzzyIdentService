
using System;

namespace FuzzyIdentService.Models.Entities
{
    public class User: IWithId
    {
        public string Id { get; set; }
        //public Guid LocationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
    }
}

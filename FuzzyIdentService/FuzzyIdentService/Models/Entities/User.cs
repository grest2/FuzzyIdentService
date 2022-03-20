
using System;

namespace FuzzyIdentService.Models.Entities
{
    public class User: IWithId
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }

        public User(Guid id,string FirstName,string MiddleName,string LastName,string Index)
        {
            this.Id = id;
            this.FirstName = FirstName;
            this.MiddleName = MiddleName;
            this.LastName = LastName;
            this.Index = Index;
        }
    }
}

using System;

namespace FuzzyIdentService.Models.Entities
{
    public interface IWithId
    {
        public Guid Id { get; set; }
    }
}
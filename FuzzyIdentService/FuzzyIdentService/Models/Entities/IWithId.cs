using System;

namespace FuzzyIdentService.Models.Entities
{
    public interface IWithId
    {
        public string Id { get; set; }
    }
}
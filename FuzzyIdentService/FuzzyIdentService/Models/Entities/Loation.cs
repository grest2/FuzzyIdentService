using System;
using System.Collections.Generic;

namespace FuzzyIdentService.Models.Entities
{
    public class Location 
    {
        public Guid Id { get; set; }
        public string Index { get; set; }
        public string Addres { get; set; }
        
        public List<User> Users { get; set; }
    }
}
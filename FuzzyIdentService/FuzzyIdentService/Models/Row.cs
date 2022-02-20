using System.Collections.Generic;

namespace FuzzyIdentService.Models
{
    public class Row<T>
    {
        public List<T> List { get; set; }
        public long Offset { get; set; }
        public long Count { get; set; }
        public long Total { get; set; }
    }
}
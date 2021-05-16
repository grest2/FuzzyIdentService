using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Abstractions
{
    interface IFuzzyHandler
    {
        public string BestMatch(string word);
    }
}

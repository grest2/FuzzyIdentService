using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Abstractions
{
    interface IFuzzyHandler
    {
        public int BestMatch(string FirstWord,string SecondWord);
    }
}

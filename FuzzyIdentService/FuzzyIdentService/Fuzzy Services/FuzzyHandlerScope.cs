using FuzzyIdentService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Fuzzy_Services
{
    public class FuzzyHandlerScope : IFuzzyHandler
    {
        RussianMetaphone rusMetaphone = new RussianMetaphone();
        public string BestMatch(string word)
        {
            string fuzzyWord = rusMetaphone.getRightName(word);

        }
    }
}

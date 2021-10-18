using FuzzyIdentService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Fuzzy_Services
{
    public class FuzzyHandlerScope : IFuzzyHandler
    {
        RussianMetaphone rusMetaphone = RussianMetaphone.getInstance();
        DamerauLevensteinMetric dlMetric = new DamerauLevensteinMetric();

        public int BestMatch(string FirstWord, string SecondWord)
        {
            string FoneticName = rusMetaphone.getRightName(FirstWord);

            string FoneticNameSecond = rusMetaphone.getRightName(SecondWord);
            
            return dlMetric.DamerauLevensteinMetrics(FoneticName,FoneticNameSecond);

        }
    }
}

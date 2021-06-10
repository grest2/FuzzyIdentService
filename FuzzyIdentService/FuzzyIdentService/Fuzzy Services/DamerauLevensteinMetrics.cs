using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Fuzzy_Services
{
    public class DamerauLevensteinMetric
    {
        private int Minimum(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        private int Minimum(int a, int b, int c)
        {
            if (a < b)
            {
                if (a < c)
                {
                    return a;
                }
                else
                {
                    return c;
                }
            }
            else if (b < c)
            {
                return b;
            }
            else
            {
                return c;
            }
        }
        public int DamerauLevensteinMetrics(string firstName, string secondName)
        {
            int fnLength = firstName.Length + 1;
            int snLength = secondName.Length + 1;
            int[,] symbolMetrics = new int[fnLength, snLength];
            for (int i = 0; i < fnLength; i++)
            {
                symbolMetrics[i, 0] = i;
            }
            for (int j = 0; j < snLength; j++)
            {
                symbolMetrics[0, j] = j;
            }
            for (int i = 1; i < fnLength; i++)
            {
                for (int j = 1; j < snLength; j++)
                {
                    var cost = firstName[i - 1] == secondName[j - 1] ? 0 : 1;

                    symbolMetrics[i, j] = Minimum(symbolMetrics[i - 1, j] + 1,
                                                  symbolMetrics[i, j - 1] + 1,
                                                  symbolMetrics[i - 1, j - 1]
                                                  + cost);
                    if (i > 1 && j > 1 && firstName[i - 1] == secondName[j - 2] && firstName[i - 2] == secondName[j - 1])
                    {
                        symbolMetrics[i, j] = Minimum(symbolMetrics[i, j], symbolMetrics[i - 2, j - 2 + cost]);
                    }
                }
            }
            return symbolMetrics[fnLength - 1, snLength - 1];
        }
    }
}

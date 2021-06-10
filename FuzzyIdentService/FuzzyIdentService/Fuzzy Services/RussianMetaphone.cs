using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FuzzyIdentService.Fuzzy_Services
{
    static class MetaphoneHelper
    {
        public static string ReplaceChar(this string s, char c)
        {
            return s.Substring(0, s.Length - 1) + c;
        }
        public static char LastChar(this string s)
        {
            return s[s.Length - 1];
        }
    }

    public class RussianMetaphone
    {
        private const string Path = "";
        private static RussianMetaphone instance;
        string alphavite = string.Join("", File.ReadAllLines("alphavite.txt"));
        string sonorous = string.Join("", File.ReadAllLines("sonorous.txt"));
        string blinds = string.Join("", File.ReadAllLines("blinds.txt"));
        string consonstants = string.Join("", File.ReadAllLines("consonstants.txt"));
        string vowels = string.Join("", File.ReadAllLines("vowels.txt"));
        string replaces = string.Join("", File.ReadAllLines("replaces.txt"));
        string[] dictKeys = File.ReadAllLines("Dictionary.txt");
        string[] dictValues = File.ReadAllLines("DictValues.txt");
        static Dictionary<string, string> suffixDictionary = new Dictionary<string, string>();

        public RussianMetaphone()
        {
            for (int index = 0; index < dictKeys.Length; index++)
            {
                suffixDictionary.Add(dictKeys[index], dictValues[index]);
            }
        }
        public static RussianMetaphone getInstance()
        {
            if (instance == null) {
                instance = new RussianMetaphone();
            }
            return instance;
        }
        public string getRightName(string name)
        {

            name = name.ToUpper();
            var stringBuilder = new StringBuilder(" ");
            for (int i = 0; i < name.Length; i++)
            {
                if (alphavite.Contains(name[i]))
                {
                    stringBuilder.Append(name[i]);
                }
            }
            var reStringName = stringBuilder.ToString();
            foreach (var item in suffixDictionary)
            {
                if (!reStringName.EndsWith(item.Key)) continue;
                reStringName = Regex.Replace(name, item.Key + "$", item.Value);
            }

            var indexBlinds = sonorous.IndexOf(name.LastChar());
            if (indexBlinds != -1)
            {
                reStringName = reStringName.ReplaceChar(blinds[indexBlinds]);
            }
            var oldChar = ' ';
            string rightName = "";
            for (int i = 0; i < reStringName.Length; i++)
            {
                var charFromString = reStringName[i];
                var vowelsIndex = vowels.IndexOf(charFromString);
                if (vowelsIndex != -1)
                {
                    if (oldChar == 'Й' || oldChar == 'И')
                    {
                        if (charFromString == 'О' || charFromString == 'И')
                        {
                            oldChar = 'И';
                            rightName.ReplaceChar(oldChar);
                        }
                        else
                        {
                            if (charFromString != oldChar)
                            {
                                rightName = rightName + replaces[vowelsIndex];
                            }
                        }
                    }
                    else
                    {
                        if (charFromString != oldChar)
                        {
                            rightName = rightName + replaces[vowelsIndex];
                        }
                    }
                }
                else
                {
                    if (charFromString != oldChar)
                    {
                        if (consonstants.Contains(charFromString))
                        {
                            var consonstantsIndex = blinds.IndexOf(oldChar);
                            if (consonstantsIndex != -1)
                            {
                                oldChar = sonorous[consonstantsIndex];
                                rightName = rightName.ReplaceChar(oldChar);
                            }
                        }
                        if (charFromString != oldChar)
                        {
                            rightName = rightName + charFromString;
                        }
                    }
                }
                oldChar = charFromString;
            }
            return rightName;
        }
    }
}

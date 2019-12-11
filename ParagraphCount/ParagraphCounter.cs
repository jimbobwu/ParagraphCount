using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParagraphCount
{
    public class ParagraphCounter
    {
        public const string DefaultSeparator = ",.!?;:-()\"\' ";
        public Dictionary<string, int> GetWordCounts(string input, char[] separators)
        {
            var splitArray = input.Split(separators).Where(n => !string.IsNullOrEmpty(n)).ToList();
            var dict = splitArray.GroupBy(n => n.ToLower()).ToDictionary(n => n.Key, p => p.Count());
            return dict;
        }
        public Dictionary<string, int> GetWordCounts(string input, string separators = DefaultSeparator)
        {
            return GetWordCounts(input, separators.ToCharArray());
        }
    }
}

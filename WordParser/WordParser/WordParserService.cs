using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordParser
{
    public class WordParserService
    {
        public Dictionary<string, int> Parse(string input)
        {
            var words = input.Split(new [] {',', '.', ';', '!', '?', ' '}, StringSplitOptions.RemoveEmptyEntries);
            return words
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .ToDictionary(g => g.Key, g=> g.Count());
        }
    }
}

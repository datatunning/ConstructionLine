using System;

namespace WordParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "Hello, world world !!! This is me me.";
            var wordParser = new WordParserService();
            var wordCount = wordParser.Parse(input);
            foreach (var kv in wordCount)
            {
                Console.WriteLine($"{kv.Key} -> {kv.Value}");
            }
        }
    }
}

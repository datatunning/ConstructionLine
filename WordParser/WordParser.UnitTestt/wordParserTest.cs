using System;
using Xunit;
using Xunit.Sdk;

namespace WordParser.UnitTest
{
    public class WordParserTest
    {
        [Fact]
        public void Input_Should_Count_The_Words()
        {
            var input = "Hello, world world !!! This is me me.";
            var wordParser = new WordParserService();
            var wordCount = wordParser.Parse(input);

            Assert.Equal(2, wordCount["me"]);
            Assert.Equal(2, wordCount["world"]);
            Assert.Equal(1, wordCount["Hello"]);
            Assert.Equal(1, wordCount["is"]);
            Assert.Equal(1, wordCount["This"]);
            Assert.Equal(5, wordCount.Count);
        }
    }
}

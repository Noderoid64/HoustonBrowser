using System;
using Xunit;
using HoustonBrowser.Parsing;
using System.Collections.Generic;

namespace HoustonBrowser.Parsing.Tests
{
    public class ParsingTest
    {
        [Theory]
        [InlineData("<tagname>?<tagname/>")]
        private void LexAnalyserTests(string page)
        {
            HtmlLexAnalyser lexAnalyser = new HtmlLexAnalyser(page);
            Token cache = new Token(-1,"");
            List<Token> tokens = new List<Token>();
            while (cache.Type != (int)Enums.TokenType.EOF)
            {
                cache = lexAnalyser.Tokenize();
                cache.Standartize();
                tokens.Add(cache);
            }
        }
    }
}

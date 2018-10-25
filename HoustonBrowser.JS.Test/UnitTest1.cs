using System;
using Xunit;
using HoustonBrowser.JS;

namespace HoustonBrowser.JS.Test
{
    public class UnitTest1
    {
        [Fact]
        public void FunctionTest1()
        {           
            ESParser e = new ESParser();
            Token[] a = {
                new Token(TokenType.Keyword,"function"),
                new Token(TokenType.Identifier,"b"),
                new Token(TokenType.Punctuator,"("),
                new Token(TokenType.Punctuator,")"),
                new Token(TokenType.Punctuator,"{"),
                new Token(TokenType.Punctuator,"}"),
                new Token(TokenType.Keyword,"function")
            };
            Assert.False(e.Program(a));            
        }

        [Fact]
        public void FunctionTest2()
        {           
            ESParser e = new ESParser();
            Token[] a = {
                new Token(TokenType.Keyword,"function"),
                new Token(TokenType.Identifier,"a"),
                new Token(TokenType.Punctuator,"("),
                new Token(TokenType.Punctuator,")"),
                new Token(TokenType.Punctuator,"{"),
                new Token(TokenType.Keyword,"var"),
                new Token(TokenType.Identifier,"v"),
                new Token(TokenType.Punctuator,"="),
                new Token(TokenType.Literal,"555"),
                new Token(TokenType.Punctuator,";"),
                new Token(TokenType.Identifier,"v"),
                new Token(TokenType.Punctuator,"+"),
                new Token(TokenType.Literal,"10"),
                new Token(TokenType.Punctuator,";"),
                new Token(TokenType.Punctuator,"}"),
            };
            Assert.True(e.Program(a));            
        }
    }
}

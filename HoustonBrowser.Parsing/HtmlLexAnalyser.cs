using System;
using System.Collections.Generic;
using System.Text;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM;
using HoustonBrowser.Parsing.Interfaces;

namespace HoustonBrowser.Parsing
{
    class HtmlLexAnalyser:ILexAnalyser<Token<string>>
    {
        private int currentState;
        Stack<Node> stackOfOpenElements;

        HtmlLexAnalyser()
        {
            currentState = (int) TokenStates.Data;
            stackOfOpenElements = new Stack<Node>();
        }

        Token<string> ILexAnalyser<Token<string>>.Tokenize(string document)
        {
            Token<string> token= new Token<string>("","");
            for (int i = 0; i < document.Length; i++)
            {
                switch (currentState)
                {
                    case (int) TokenStates.Data:
                        {
                            switch (document[i + 1])
                            {
                                case '&':
                                    {

                                        break;
                                    }
                                case '<':
                                    {

                                        break;
                                    }
                                default:
                                    {

                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
            return token;
        }
    }
}

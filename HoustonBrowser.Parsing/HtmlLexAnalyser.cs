using System;
using System.Collections.Generic;
using System.Text;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM;
using HoustonBrowser.Parsing.Interfaces;

namespace HoustonBrowser.Parsing
{
    class HtmlLexAnalyser:ILexAnalyser<Token<int>>
    {
        private int currentState;
        private int currentSymbol;
        Stack<Node> stackOfOpenElements;

        HtmlLexAnalyser()
        {
            currentState = (int) TokenStates.Data;
            stackOfOpenElements = new Stack<Node>();
            currentSymbol = 0;
        }

        Token<int> ILexAnalyser<Token<int>>.Tokenize(string document)
        {
            Token<int> token= new Token<int>(0,"");
            for (; currentSymbol < document.Length; currentSymbol++)
            {
                switch (currentState)
                {
                    case (int) TokenStates.Data:
                        {
                            switch (document[currentSymbol + 1])
                            {
                                case '<':
                                    {
                                        currentState = (int)TokenStates.TagOpen;
                                        break;
                                    }
                                default:
                                    {
                                        
                                        break;
                                    }
                            }
                            break;
                        }
                    case (int) TokenStates.TagOpen:
                    {
                        switch (document[currentSymbol + 1])
                            {
                                case '/':
                                    {
                                        currentState = (int)TokenStates.TagOpen;
                                        break;
                                    }
                                case '!':
                                    {

                                        break;
                                    }
                                default:
                                    {
                                        currentState = (int)TokenStates.TagName;
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

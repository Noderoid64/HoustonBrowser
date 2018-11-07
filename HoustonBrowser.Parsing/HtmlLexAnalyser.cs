using System;
using System.Collections.Generic;
using System.Text;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM;
using HoustonBrowser.Parsing.Interfaces;

namespace HoustonBrowser.Parsing
{
    class HtmlLexAnalyser
    {
        private string cache;
        private int currentState;
        private int currentSymbol;
        Stack<Node> stackOfOpenElements;
        private int insertionState;
        private bool isTagOpen;
        private string document;

        public HtmlLexAnalyser(string doc)
        {
            document = doc;
            currentState = (int) TokenStates.Data;
            stackOfOpenElements = new Stack<Node>();
            currentSymbol = 0;
            cache = "";
            isTagOpen = true;
            insertionState = (int)InsertionModes.Initial;
        }

        public int InsertionState {set => insertionState = value; }

        public Token Tokenize()
        {
            Token token;
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
                                        if((int)InsertionModes.InBody== insertionState)
                                            currentState = (int)TokenStates.Text;
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
                                        currentState = (int)TokenStates.EndTagOpen;
                                        break;
                                    }
                                case '!':
                                    {
                                        currentState = (int)TokenStates.DoctypeName;
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
                    case (int) TokenStates.TagName:
                    {
                        switch (document[currentSymbol + 1])
                            {
                                case '>':
                                {
                                    currentState = (int)TokenStates.Data;
                                    if(isTagOpen)
                                    {
                                        token = new Token((int)TokenType.NameOfTag,cache);
                                    }
                                    else
                                    {
                                        token = new Token((int)TokenType.NameOfTagClosing,cache);
                                    }
                                    cache = "";
                                    return token;
                                }
                                default:
                                {
                                    cache += document[currentSymbol + 1];
                                    break;
                                }
                            }
                        break;
                    }
                    case (int) TokenStates.DoctypeName:
                    {
                        switch (document[currentSymbol + 1])
                        {
                            case '>':
                                {
                                    currentState = (int)TokenStates.Data;
                                    break;
                                }
                                default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                    case (int) TokenStates.EndTagOpen:
                    {
                        switch (document[currentSymbol + 1])
                        {
                            default:
                                    {
                                        currentState = (int)TokenStates.TagName;
                                        break;
                                    }
                            }
                        break;
                    }
                    case (int)TokenStates.Text:
                        {
                            switch (document[currentSymbol + 1])
                            {
                                case '<':
                                    {
                                        currentState = (int)TokenStates.TagOpen;
                                        token = new Token((int)TokenType.Text, cache);
                                        cache = "";
                                        return token;
                                    }
                                default:
                                    {
                                        cache += document[currentSymbol + 1];
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
            return token = new Token((int)TokenType.EOF,"");
        }
        
    }
}

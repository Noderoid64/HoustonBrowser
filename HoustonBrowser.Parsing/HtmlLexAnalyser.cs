using System;
using System.Collections.Generic;
using System.Text;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.Core;
using HoustonBrowser.Parsing.Interfaces;

namespace HoustonBrowser.Parsing
{
    class HtmlLexAnalyser
    {
        private string cache;
        private int currentState;
        private int currentSymbol;
        private int insertionState;
        private bool isTagOpen;
        private string document;

        public HtmlLexAnalyser(string doc)
        {
            document=doc;
            currentState=(int)TokenStates.Data;
            currentSymbol=0;
            cache="";
            isTagOpen=true;
            insertionState=(int)InsertionModes.Initial;
        }

        public int InsertionState { set => insertionState=value; }

        public Token Tokenize()
        {
            if (document != null)
            {
                document = document.Replace('\n', ' ');
                document = document.Replace('\r', ' ');
                document = document.Replace('\t', ' ');
                document = document.Replace("  ", " ");
                while (document.IndexOf("<!--") != -1)
                {
                    document = document.Remove(document.IndexOf("<!--"), document.IndexOf("-->") - document.IndexOf("<!--"));
                }
                Token token;
                for (; currentSymbol <document.Length; currentSymbol++)
                {
                    switch (currentState)
                    {
                        case (int)TokenStates.Data:
                            {
                                switch (document[currentSymbol])
                                {
                                    case '<':
                                        {
                                            currentState=(int)TokenStates.TagOpen;
                                            break;
                                        }
                                    default:
                                        {
                                            if ((int)InsertionModes.InBody == insertionState)
                                            {
                                                currentState=(int)TokenStates.Text;
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.TagOpen:
                            {
                                isTagOpen=true;
                                switch (document[currentSymbol])
                                {
                                    case '/':
                                        {
                                            currentState=(int)TokenStates.EndTagOpen;
                                            break;
                                        }
                                    case '!':
                                        {
                                            currentState=(int)TokenStates.DoctypeName;
                                            break;
                                        }
                                    default:
                                        {
                                            currentState=(int)TokenStates.TagName;
                                            cache="";
                                            cache += document[currentSymbol];
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.TagName:
                            {

                                switch (document[currentSymbol])
                                {
                                    case ' ':
                                        {
                                            currentState=(int)TokenStates.AttributeName;
                                            if (isTagOpen)
                                            {
                                                token=new Token((int)TokenType.NameOfTag, cache);
                                            }
                                            else
                                            {
                                                token=new Token((int)TokenType.NameOfTagClosing, cache);
                                            }
                                            cache="";
                                            return token;
                                        }
                                    case '>':
                                        {
                                            currentState=(int)TokenStates.Data;
                                            if (isTagOpen)
                                            {
                                                token=new Token((int)TokenType.NameOfTag, cache);
                                            }
                                            else
                                            {
                                                token=new Token((int)TokenType.NameOfTagClosing, cache);
                                            }
                                            cache="";
                                            return token;
                                        }
                                    default:
                                        {
                                            cache += document[currentSymbol];
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.DoctypeName:
                            {
                                switch (document[currentSymbol])
                                {
                                    case '>':
                                        {
                                            currentState=(int)TokenStates.Data;
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.EndTagOpen:
                            {
                                isTagOpen=false;
                                switch (document[currentSymbol])
                                {
                                    default:
                                        {
                                            cache += document[currentSymbol];
                                            currentState=(int)TokenStates.TagName;
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.Text:
                            {
                                switch (document[currentSymbol])
                                {
                                    case '<':
                                        {
                                            currentState=(int)TokenStates.TagOpen;
                                            if (TokenCheck(cache))
                                            {
                                                token=new Token((int)TokenType.Text, cache);
                                                cache="";
                                                currentSymbol++;
                                                return token;
                                            }
                                            cache="";
                                            break;
                                        }
                                    default:
                                        {
                                            cache += document[currentSymbol];
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.Attributes:
                            {
                                switch (document[currentSymbol])
                                {
                                    case '>':
                                        {
                                            currentState=(int)TokenStates.Data;
                                            if (currentSymbol + 1 <document.Length && document[currentSymbol + 1] != '<')
                                            {
                                                cache += document[currentSymbol + 1];
                                            }
                                            break;
                                        }
                                    case '=':
                                        {
                                            currentSymbol++;
                                            currentState=(int)TokenStates.AttributeValue;
                                            cache="";
                                            break;
                                        }
                                    case ' ':
                                        {
                                            currentState=(int)TokenStates.AttributeName;
                                            cache="";
                                            break;
                                        }
                                    default:
                                        {

                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.AttributeName:
                            {
                                switch (document[currentSymbol])
                                {
                                    case '>':
                                        {
                                            currentState=(int)TokenStates.Data;
                                            if (currentSymbol + 1 <document.Length && document[currentSymbol + 1] != '<')
                                            {
                                                cache += document[currentSymbol + 1];
                                            }
                                            break;
                                        }
                                    case ' ':
                                        {
                                            break;
                                        }
                                    case '=':
                                        {
                                            currentState=(int)TokenStates.Attributes;
                                            Token t=new Token((int)TokenType.AttributeName, cache);
                                            cache="";
                                            return t;
                                        }
                                    default:
                                        {
                                            cache += document[currentSymbol];
                                            break;
                                        }
                                }
                                break;
                            }
                        case (int)TokenStates.AttributeValue:
                            {
                                switch (document[currentSymbol])
                                {
                                    case '>':
                                        {
                                            currentState=(int)TokenStates.Data;
                                            if (currentSymbol + 1 <document.Length && document[currentSymbol + 1] != '<')
                                            {
                                                cache += document[currentSymbol + 1];
                                            }
                                            break;
                                        }
                                    case '"':
                                        {
                                            currentState=(int)TokenStates.Attributes;
                                            Token t=new Token((int)TokenType.AttributeName, cache);
                                            cache="";
                                            return t;
                                        }
                                    default:
                                        {
                                            cache += document[currentSymbol];
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                }
                return token=new Token((int)TokenType.EOF, "");
            }
            return new Token((int)TokenType.EOF, "");
        }
        private bool TokenCheck(string line)
        {
            string TokenCheck=line;
            for (int i=0; i <TokenCheck.Length; i++)
            {
                if (TokenCheck[i] == ' ')
                {
                    TokenCheck=TokenCheck.Replace(" ", "");
                }
            }
            if (TokenCheck == "")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
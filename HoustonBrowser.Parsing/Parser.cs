using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public class Parser:IParser
    {
        private string HTMLDoc;
        private string attributeName;

        public Parser()
        {
            
        }

        public event EventHandler<string> onNonHtmlEvent;

        public string Parse()
        {
            //HTMLDOM domTree = new HTMLDOM();
            /*Conditions conditions;
            int currentCondition = 0,currentSymIndex = 0;
            bool endOfDocument = false;
            string currentTag = "";
            while(!endOfDocument)
            {
                switch(currentCondition)
                {
                    case (int)Conditions.Data://Определение состояния
                    {
                        if(HTMLDoc[currentSymIndex]=='<')
                        {
                            if(currentSymIndex + 1 < HTMLDoc.Length)
                            {
                                if(HTMLDoc[currentSymIndex + 1]=='/')
                                {
                                    currentCondition = (int)Conditions.CloseTagStart;
                                }
                                else
                                {
                                    currentCondition = (int)Conditions.OpenTagStart;
                                }
                            }
                        }
                        break;
                    }
                }
                currentSymIndex++;
            }*/
            //return domTree;
           
            
            return "";
        }
        public HTMLDocument Parse(string value)
        {
            List<Node> stackOfOpenedElements = new List<Node>();
            int insertMode = (int)InsertionModes.Initial;
            //int currentTemplateInsertMode = (int)InsertionModes.Initial;
            List<int> StackOfTemplateInsertModesUsed = new List<int>();
            List<Node> listOfOpenTags = new List<Node>();
            List<Token> tokens = new List<Token>();
            HTMLDocument doc = new HTMLDocument();
            HtmlLexAnalyser lexAnalyser = new HtmlLexAnalyser(value);

            bool last = false;
            Stack<Node> nodes = new Stack<Node>();
            while (!last)
            {
                lexAnalyser.InsertionState = insertMode;
                Token token = lexAnalyser.Tokenize();
                tokens.Add(token);
                switch (token.Type)
                {
                    case (int)TokenType.EOF:
                        {
                            last = true;
                            break;
                        }
                    case (int)TokenType.NameOfTag:
                        {
                            switch (insertMode)
                            {
                                case (int)InsertionModes.Initial:
                                    {
                                        switch (token.Value.ToLower())
                                        {
                                            case "html":
                                                {                                                    
                                                    doc = new HTMLDocument();
                                                    nodes.Push(doc);
                                                    break;
                                                }
                                            case "head":
                                                {
                                                    var item = new HTMLHeadElement();
                                                    nodes.Peek().AppendChild(item);
                                                    nodes.Push(item);
                                                    break;
                                                }
                                            case "body":
                                                {
                                                    insertMode = (int)InsertionModes.InBody;
                                                    var item = new HTMLBodyElement();
                                                    nodes.Peek().AppendChild(item);
                                                    nodes.Push(item);
                                                    break;
                                                }
                                            default:
                                                {
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case (int)InsertionModes.BeforeHtml:
                                    {

                                        break;
                                    }
                                case (int)InsertionModes.BeforeHead:
                                    {

                                        break;
                                    }
                                case (int)InsertionModes.AfterHead:
                                    {

                                        break;
                                    }
                                case (int)InsertionModes.InBody:
                                    {
                                        switch (token.Value.ToLower())
                                        {
                                            case "p":
                                                {
                                                    var item = new HTMLParagraphElement();
                                                    nodes.Peek().AppendChild(item);
                                                    nodes.Push(item);
                                                    break;
                                                }
                                            case "script":
                                                {
                                                    var item = new HTMLScriptElement();
                                                    nodes.Peek().AppendChild(item);
                                                    nodes.Push(item);
                                                    break;
                                                }
                                            case "div":
                                                {
                                                    var item = new HTMLDivElement();
                                                    nodes.Peek().AppendChild(item);
                                                    nodes.Push(item);
                                                    break;
                                                }
                                            case "button":
                                                {
                                                    var item = new HTMLButtonElement();
                                                    nodes.Peek().AppendChild(item);
                                                    nodes.Push(item);
                                                    break;
                                                }
                                            default:
                                                {
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case (int)InsertionModes.AfterBody:
                                    {

                                        break;
                                    }
                            }
                            break;
                        }
                    case (int)TokenType.NameOfTagClosing:
                        {
                            if (nodes.Count != 0)
                            {
                                nodes.Pop();
                            }    
                            break;
                        }
                    case (int)TokenType.AttributeName:
                        {
                            attributeName = token.Value;
                            break;
                        }
                    case (int)TokenType.AttributeValue:
                        {
                            switch(attributeName.ToLower())
                            {
                                case "src":
                                {
                                    nodes.Peek().Attributes.SetNamedItem(new Attr("src",token.Value));
                                    break;
                                }
                                default:
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    case (int)TokenType.Text:
                        {
                            switch (nodes.Peek().NodeName)
                            {
                                case "script":
                                    onNonHtmlEvent?.Invoke(this, token.Value);
                                    break;

                                case "button":
                                    nodes.Peek().NodeValue = token.Value;
                                    break;

                                default:
                                    var item = new Text(token.Value);
                                    nodes.Peek().AppendChild(item);
                                    break;
                            }                            
                            break;
                        }
                    case (int)TokenType.Null:
                        {
                            throw new Exception("Raw token got.");
                        }

                }
            }
            int x = tokens.Capacity;
            return doc;
        }

    }
}

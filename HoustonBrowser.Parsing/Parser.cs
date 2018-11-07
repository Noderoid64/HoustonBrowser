using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public class Parser:IParser
    {
        private string HTMLDoc;

        public Parser()
        {
            
        }

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
        public Document Parse(string value)
        {
            List<Node> stackOfOpenedElements = new List<Node>();
            int insertMode = (int)InsertionModes.Initial;
            int currentTemplateInsertMode = (int)InsertionModes.Initial;
            List<int> StackOfTemplateInsertModesUsed = new List<int>();
            List<Node> listOfOpenTags = new List<Node>();
            List<Token> tokens = new List<Token>();
            Document doc = new Document();
            HtmlLexAnalyser lexAnalyser = new HtmlLexAnalyser(value);
            //1
            bool last = false;
            //2
            //List<Node> stackOfOpenedElements = new List<Node>();
            //stackOfOpenedElements.Add(new Node());
            //3
            //Node ancestor = new Node();
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
                                                    //listOfOpenTags.Add(new HTMLTags.HTMLHtmlElement());
                                                    //doc.
                                                    break;
                                                }
                                            case "head":
                                                {
                                                    break;
                                                }
                                            case "body":
                                                {
                                                    insertMode = (int)InsertionModes.InBody;
                                                    break;
                                                }
                                            case "p":
                                                {
                                                    break;
                                                }
                                            case "div":
                                                {
                                                    break;
                                                }
                                            case "button":
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
                            //
                            break;
                        }
                    case (int)TokenType.Text:
                        {
                            //
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

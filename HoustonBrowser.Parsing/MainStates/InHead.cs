using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public class InHead:State,IState
    {
        string attributeName;
        string lastNonCloseTagOpened;

        public event EventHandler<string> OnNonHtmlEvent;

        private Dictionary<string, TagProcessing> openTagsDict;
        private Dictionary<string, TagProcessing> closeTagsDict;
        //private Dictionary<string, TagProcessing> nameAttributeDict;

        public InHead()
        {
            openTagsDict = new Dictionary<string, TagProcessing>();
            closeTagsDict = new Dictionary<string, TagProcessing>();
            lastNonCloseTagOpened = "";
            attributeName = "";
            //Opening tags
            openTagsDict.Add("title",titleOpenProcessing);
            openTagsDict.Add("script",scriptOpenProcessing);

            openTagsDict.Add("link", linkOpenProcessing);
            //Closing tags
            closeTagsDict.Add("title", titleCloseProcessing);
            closeTagsDict.Add("script",scriptCloseProcessing);
            closeTagsDict.Add("head", headCloseProcessing);

            //Attributes names

            //Attributes values
        }
        public new void ProcessToken(Token token)
        {
                switch (token.Type)
                {
                    case (int)TokenType.NameOfTag:
                        {
                            if (lastNonCloseTagOpened != "")
                            {
                                if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == lastNonCloseTagOpened)
                                {
                                    StatesData.OpenedTags.Pop();
                                    lastNonCloseTagOpened = "";
                                }
                            }
                            if (openTagsDict.ContainsKey(token.Value))
                            {
                                openTagsDict.GetValueOrDefault(token.Value).Invoke();
                            }
                            //write exc
                            break;
                        }
                    case (int)TokenType.NameOfTagClosing:
                        {
                            if (lastNonCloseTagOpened != "")
                            {
                                if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == lastNonCloseTagOpened)
                                {
                                    StatesData.OpenedTags.Pop();
                                    lastNonCloseTagOpened = "";
                                }
                        }
                        if (closeTagsDict.ContainsKey(token.Value))
                            {
                                closeTagsDict.GetValueOrDefault(token.Value).Invoke();
                            }
                            //write exc
                            break;
                        }
                    case (int)TokenType.AttributeName:                 
                        {
                            attributeName = token.Value;
                            break;
                        }
                    case (int)TokenType.AttributeValue:
                        {
                            StatesData.OpenedTags.Peek().Attributes.SetNamedItem(new Attr(attributeName, token.Value));
                            break;
                        }
                case (int)TokenType.Text:
                    {
                        if (StatesData.OpenedTags.Count != 0)
                        {
                            if (StatesData.OpenedTags.Peek().NodeName == "script")
                            {
                                OnNonHtmlEvent?.Invoke(this, token.Value);
                            }
                            else
                            {
                                var item = new Text(token.Value);
                                StatesData.OpenedTags.Peek().AppendChild(item);
                            }
                        }
                        break;
                    }
                case (int)TokenType.EOF:
                    {
                        StatesData.FinishParsing();
                        break;
                    }
            }
            
        }
        private void titleOpenProcessing()
        {
            AddingStructureTag("title");
            openTagsDict.Remove("title");
        }
        private void titleCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "title")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag title");
            }
        }
        private void scriptOpenProcessing()
        {
            var item = new HTMLScriptElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void scriptCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "script")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag script");
            }
        }
        private void headCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "head")
            {
                StatesData.OpenedTags.Pop();
                StatesData.currentState = (int)InsertionModes.Initial;
            }
            else
            {
                Console.WriteLine("Error with closing tag head");
            }
        }
        private void linkOpenProcessing()
        {
            AddingStructureTag("link");
            lastNonCloseTagOpened = "link";
        }
    }
}
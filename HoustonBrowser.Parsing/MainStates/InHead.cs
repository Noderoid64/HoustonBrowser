using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.HTML;

namespace HoustonBrowser.Parsing
{
    public class InHead:State,IState
    {
        string attributeName;
        string lastNonCloseTagOpened;

        public event EventHandler<string> onNonHtmlEvent;

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
            openTagsDict.Add("title",TITLEOpenProcessing);
            openTagsDict.Add("script",SCRIPTOpenProcessing);

            openTagsDict.Add("link", LINKOpenProcessing);
            //Closing tags
            closeTagsDict.Add("title", TITLECloseProcessing);
            closeTagsDict.Add("script",SCRIPTCloseProcessing);
            closeTagsDict.Add("head", HEADCloseProcessing);

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
                                if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == lastNonCloseTagOpened)
                                {
                                    StatesData.openedTags.Pop();
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
                            StatesData.openedTags.Peek().Attributes.SetNamedItem(new Attr(attributeName, token.Value));
                            break;
                        }
                case (int)TokenType.Text:
                    {
                        if (StatesData.openedTags.Count != 0)
                        {
                            if (StatesData.openedTags.Peek().NodeName == "script")
                            {
                                onNonHtmlEvent?.Invoke(this, token.Value);
                            }
                            else
                            {
                                var item = new Text(token.Value);
                                StatesData.openedTags.Peek().AppendChild(item);
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
        private void TITLEOpenProcessing()
        {
            AddingStructureTag("title");
            openTagsDict.Remove("title");
        }
        private void TITLECloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "title")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag title");
            }
        }
        private void SCRIPTOpenProcessing()
        {
            var item = new HTMLScriptElement();
            StatesData.openedTags.Peek().AppendChild(item);
            StatesData.openedTags.Push(item);
        }
        private void SCRIPTCloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "script")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag script");
            }
        }
        private void HEADCloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "head")
            {
                StatesData.openedTags.Pop();
                StatesData.currentState = (int)InsertionModes.Initial;
            }
            else
            {
                Console.WriteLine("Error with closing tag head");
            }
        }
        private void LINKOpenProcessing()
        {
            AddingStructureTag("link");
            lastNonCloseTagOpened = "link";
        }
    }
}
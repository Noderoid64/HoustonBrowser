using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.HTML;

namespace HoustonBrowser.Parsing
{
    public class InBody:State,IState
    {
        string attributeName;
        string lastNonCloseTagOpened;

        private Dictionary<string, TagProcessing> openTagsDict;
        private Dictionary<string, TagProcessing> closeTagsDict;

        public InBody()
        {
            openTagsDict = new Dictionary<string, TagProcessing>();
            closeTagsDict = new Dictionary<string, TagProcessing>();
            lastNonCloseTagOpened = "";
            attributeName = "";
            //Opening tags
            openTagsDict.Add("title", TITLECloseProcessing);
            openTagsDict.Add("link", LINKOpenProcessing);
            //Closing tags
            closeTagsDict.Add("title", TITLECloseProcessing);
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
                            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeValue == lastNonCloseTagOpened)
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
            }

        }
        private void TITLEOpenProcessing()
        {
            AddingStructureTag("title");
            openTagsDict.Remove("title");
        }
        private void TITLECloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeValue == "title")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag title");
            }
        }
    }
}
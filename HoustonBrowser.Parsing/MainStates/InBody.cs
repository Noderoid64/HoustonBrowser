using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.HTML;

namespace HoustonBrowser.Parsing
{
    public class InBody:State,IState
    {
        public event EventHandler<string> onNonHtmlEvent;
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
            openTagsDict.Add("h1", H1OpenProcessing);//headers
            openTagsDict.Add("h2", H2OpenProcessing);
            openTagsDict.Add("h3", H3OpenProcessing);
            openTagsDict.Add("p", POpenProcessing);//Containers
            openTagsDict.Add("a", AOpenProcessing);//link
            openTagsDict.Add("div", DIVOpenProcessing);
            openTagsDict.Add("i", IOpenProcessing);//formatting text
            openTagsDict.Add("em", POpenProcessing);
            openTagsDict.Add("script", SCRIPTOpenProcessing);
            openTagsDict.Add("button", BUTTONOpenProcessing);
            //Non closing tags
            openTagsDict.Add("img", IMGOpenProcessing);
            openTagsDict.Add("hr", HROpenProcessing);
            //Closing tags
            closeTagsDict.Add("h1", H1CloseProcessing);//headers
            closeTagsDict.Add("h2", H2CloseProcessing);
            closeTagsDict.Add("h3", H3CloseProcessing);
            closeTagsDict.Add("p", PCloseProcessing);//Containers
            closeTagsDict.Add("a", ACloseProcessing);//link
            closeTagsDict.Add("div", DIVCloseProcessing);
            closeTagsDict.Add("i", ICloseProcessing);//formatting text
            closeTagsDict.Add("em", PCloseProcessing);
            closeTagsDict.Add("body", BODYCloseProcessing);
            closeTagsDict.Add("script", SCRIPTCloseProcessing);
            closeTagsDict.Add("button", BUTTONCloseProcessing);
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
                            else if (StatesData.openedTags.Peek().NodeName == "button")
                            {
                                StatesData.openedTags.Peek().NodeValue = token.Value;
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
        #region headers
        private void H1OpenProcessing()
        {
            AddingStructureTag("h1");
        }
        private void H2OpenProcessing()
        {
            AddingStructureTag("h2");
        }
        private void H3OpenProcessing()
        {
            AddingStructureTag("h3");
        }
        private void H1CloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "h1")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h1");
            }
        }
        private void H2CloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "h2")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h2");
            }
        }
        private void H3CloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "h3")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h3");
            }
        }
        #endregion
        private void POpenProcessing()
        {
            AddingStructureTag("p");
        }
        private void PCloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "p")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag p");
            }
        }
        private void AOpenProcessing()
        {
            AddingStructureTag("a");
        }
        private void ACloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "a")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag a");
            }
        }
        private void DIVOpenProcessing()
        {
            var item = new HTMLDivElement();
            StatesData.openedTags.Peek().AppendChild(item);
            StatesData.openedTags.Push(item);
        }
        private void DIVCloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "div")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag div");
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
        private void BUTTONOpenProcessing()
        {
            var item = new HTMLButtonElement();
            StatesData.openedTags.Peek().AppendChild(item);
            StatesData.openedTags.Push(item);
        }
        private void BUTTONCloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "button")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag button");
            }
        }
        private void IOpenProcessing()
        {
            AddingStructureTag("i");
        }
        private void ICloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "i")
            {
                StatesData.openedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag i");
            }
        }
        private void IMGOpenProcessing()
        {
            AddingStructureTag("img");
            lastNonCloseTagOpened = "img";
        }
        private void HROpenProcessing()
        {
            AddingStructureTag("hr");
            lastNonCloseTagOpened = "hr";
        }
        private void BODYCloseProcessing()
        {
            if (StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeName == "body")
            {
                StatesData.openedTags.Pop();
                StatesData.currentState = (int)InsertionModes.Initial;
            }
            else
            {
                Console.WriteLine("Error with closing tag body");
            }
        }
    }
}
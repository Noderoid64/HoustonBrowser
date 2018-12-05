using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

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
            openTagsDict.Add("h1", h1OpenProcessing);//headers
            openTagsDict.Add("h2", h2OpenProcessing);
            openTagsDict.Add("h3", h3OpenProcessing);
            openTagsDict.Add("h4", h4OpenProcessing);//headers
            openTagsDict.Add("h5", h5OpenProcessing);
            openTagsDict.Add("h6", h6OpenProcessing);
            openTagsDict.Add("p", pOpenProcessing);//Containers
            openTagsDict.Add("a", aOpenProcessing);//link
            openTagsDict.Add("div", divOpenProcessing);
            openTagsDict.Add("i", iOpenProcessing);//formatting text
            openTagsDict.Add("em", emOpenProcessing);
            openTagsDict.Add("script", scriptOpenProcessing);
            openTagsDict.Add("button", buttonOpenProcessing);
            //Non closing tags
            openTagsDict.Add("img", imgOpenProcessing);
            openTagsDict.Add("hr", hrOpenProcessing);
            //Closing tags
            closeTagsDict.Add("h1", h1CloseProcessing);//headers
            closeTagsDict.Add("h2", h2CloseProcessing);
            closeTagsDict.Add("h3", h3CloseProcessing);
            closeTagsDict.Add("h4", h4CloseProcessing);//headers
            closeTagsDict.Add("h5", h5CloseProcessing);
            closeTagsDict.Add("h6", h6CloseProcessing);
            closeTagsDict.Add("p", pCloseProcessing);//Containers
            closeTagsDict.Add("a", aCloseProcessing);//link
            closeTagsDict.Add("div", divCloseProcessing);
            closeTagsDict.Add("i", iCloseProcessing);//formatting text
            closeTagsDict.Add("em", emCloseProcessing);
            closeTagsDict.Add("body", bodyCloseProcessing);
            closeTagsDict.Add("script", scriptCloseProcessing);
            closeTagsDict.Add("button", buttonCloseProcessing);
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
                                onNonHtmlEvent?.Invoke(this, token.Value);
                            }
                            else if (StatesData.OpenedTags.Peek().NodeName == "button")
                            {
                                StatesData.OpenedTags.Peek().NodeValue = token.Value;
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
        #region headers
        private void h1OpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLH1Element();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void h2OpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLH2Element();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void h3OpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLH3Element();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void h4OpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLH4Element();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void h5OpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLH5Element();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void h6OpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLH6Element();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void h1CloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "h1")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h1");
            }
        }
        private void h2CloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "h2")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h2");
            }
        }
        private void h3CloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "h3")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h3");
            }
        }
        private void h4CloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "h4")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h4");
            }
        }
        private void h5CloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "h5")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h5");
            }
        }
        private void h6CloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "h6")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag h6");
            }
        }
        #endregion
        private void pOpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLParagraphElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void pCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag p");
            }
        }
        private void aOpenProcessing()
        {
            var item = new HTMLAnchorElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void aCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "a")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag a");
            }
        }
        private void divOpenProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            var item = new HTMLDivElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void divCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "div")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag div");
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
        private void buttonOpenProcessing()
        {
            var item = new HTMLButtonElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void buttonCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "button")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag button");
            }
        }
        private void emOpenProcessing()
        {
            AddingStructureTag("em");
        }
        private void emCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "em")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag em");
            }
        }
        private void iOpenProcessing()
        {
            var item = new HTMLIElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }
        private void iCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "i")
            {
                StatesData.OpenedTags.Pop();
            }
            else
            {
                Console.WriteLine("Error with closing tag i");
            }
        }
        private void imgOpenProcessing()
        {
            AddingStructureTag("img");
            lastNonCloseTagOpened = "img";
        }
        private void hrOpenProcessing()
        {
            var item = new HTMLHRElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
            lastNonCloseTagOpened = "hr";
        }
        private void bodyCloseProcessing()
        {
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "p")
            {
                StatesData.OpenedTags.Pop();
            }
            if (StatesData.OpenedTags.Count != 0 && StatesData.OpenedTags.Peek().NodeName == "body")
            {
                StatesData.OpenedTags.Pop();
                StatesData.currentState = (int)InsertionModes.Initial;
            }
            else
            {
                Console.WriteLine("Error with closing tag body");
            }
        }
    }
}
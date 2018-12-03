using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public class InitialState:State,IState
    {

        private Dictionary<string,TagProcessing> openTagsDict;
        private Dictionary<string,TagProcessing> closeTagsDict;
        public InitialState()
        {
            openTagsDict = new Dictionary<string,TagProcessing>();
            closeTagsDict = new Dictionary<string, TagProcessing>();
            //OpenTags
            openTagsDict.Add("html",htmlOpenProcessing);
            openTagsDict.Add("head",headOpenProcessing);
            openTagsDict.Add("body",bodyOpenProcessing);
            //CloseTags
            closeTagsDict.Add("html",htmlCloseProcessing);
            
            
        }
        public new void ProcessToken(Token token)
        {
            if (openTagsDict.ContainsKey(token.Value)|| closeTagsDict.ContainsKey(token.Value))
            {
                if (token.Type == (int)TokenType.NameOfTag)
                {
                    openTagsDict.GetValueOrDefault(token.Value).Invoke();
                }
                else if (token.Type == (int)TokenType.NameOfTagClosing)
                {
                    closeTagsDict.GetValueOrDefault(token.Value).Invoke();
                }
                else
                {
                    Console.WriteLine("Detected object of not tag type before html/head/body");
                }
            }
            else if (token.Type == (int)TokenType.EOF)
            {
                StatesData.FinishParsing();
            }
            else
            {
                Console.WriteLine("Some structure problems in your html page html/head/body");
            }
        }
        private void htmlOpenProcessing()
        {                                                   
            var item = new HTMLHtmlElement();
            StatesData.Root.AppendChild(item);
            StatesData.OpenedTags.Push(item);
            openTagsDict.Remove("html");
        }
        private void headOpenProcessing()
        {
            var item = new HTMLHeadElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
            openTagsDict.Remove("head");
            StatesData.currentState = (int)InsertionModes.InHead;
        }
        private void bodyOpenProcessing()
        {
            var item = new HTMLBodyElement();
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
            openTagsDict.Remove("body");
            StatesData.currentState = (int)InsertionModes.InBody;
        }
        private void htmlCloseProcessing()
        {
            //if(StatesData.openedTags.Count != 0 && StatesData.openedTags.Peek().NodeValue == "html")
            //{
            //    StatesData.openedTags.Pop();
            //}
            //else
            //{
            //    Console.WriteLine("Error with closing tag html");
            //}
            StatesData.FinishParsing();
        }
        
    }
}
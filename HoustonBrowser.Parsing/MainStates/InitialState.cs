using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.HTML;

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
            openTagsDict.Add("html",HTMLOpenProcessing);
            openTagsDict.Add("head",HEADOpenProcessing);
            openTagsDict.Add("body",BODYOpenProcessing);
            //CloseTags
            closeTagsDict.Add("html",HTMLCloseProcessing);
            
            
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
        private void HTMLOpenProcessing()
        {                                                   
            var item = new Element("html");
            StatesData.root.AppendChild(item);
            StatesData.openedTags.Push(item);
            openTagsDict.Remove("html");
        }
        private void HEADOpenProcessing()
        {
            AddingStructureTag("head");
            openTagsDict.Remove("head");
            StatesData.currentState = (int)InsertionModes.InHead;
        }
        private void BODYOpenProcessing()
        {
            AddingStructureTag("body");
            openTagsDict.Remove("body");
            StatesData.currentState = (int)InsertionModes.InBody;
        }
        private void HTMLCloseProcessing()
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
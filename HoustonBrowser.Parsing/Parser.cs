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
        HTMLDocument doc;
        public Parser()
        {
        }

        public event EventHandler<string> onNonHtmlEvent;

        public string Parse()
        {   
            return "";
        }
        public HTMLDocument Parse(string value, HTMLDocument document)
        {
            List<Node> stackOfOpenedElements = new List<Node>();
            List<int> StackOfTemplateInsertModesUsed = new List<int>();
            List<Node> listOfOpenTags = new List<Node>();
            List<Token> tokens = new List<Token>();
            
            HTMLDocument doc = document;

            HtmlLexAnalyser lexAnalyser = new HtmlLexAnalyser(value);
            Stack<Node> nodes=new Stack<Node>();

            State state = new State();//main parse class
            StatesData.Reload();//IF SOME PROBLEMS DETECTED CHECK THIS METHOD
            StatesData.SetData(nodes, doc);//Adding data
            StatesData.InBody.onNonHtmlEvent += (caller, args) =>
            {
                onNonHtmlEvent(caller, args);
            };
            while (!StatesData.IsLast)
            {
                lexAnalyser.InsertionState=StatesData.currentState;
                Token token=lexAnalyser.Tokenize();
                tokens.Add(token);
                token.Standartize();
                state.ProcessToken(token);
            }
            //int x = tokens.Capacity;
            //doc.ToString();
            return doc;
        }

    }
}

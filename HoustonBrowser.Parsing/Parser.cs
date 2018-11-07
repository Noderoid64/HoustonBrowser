using System;
using HoustonBrowser.DOM;
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
            Stack<Node> stackOfOpenedElements = new Stack<Node>();
            int insertMode = (int)InsertionModes.Initial;
            int currentTemplateInsertMode = (int)InsertionModes.Initial;
            List<int> StackOfTemplateInsertModesUsed = new List<int>();
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
                Token token = lexAnalyser.Tokenize();
                switch (token.Type)
                {
                    //case TokenType.EOF:
                }
            }

            return doc;
        }

    }
}

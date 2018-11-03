using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;

namespace HoustonBrowser.Parser
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
            //1
            bool last = false;
            //2
            //List<Node> stackOfOpenedElements = new List<Node>();
            //stackOfOpenedElements.Add(new Node());
            //3
            Node ancestor = new Node();
            while (!last)
            {
                if (!ancestor.Equals(stackOfOpenedElements.Peek()))
                {
                    last = true;
                }
                //4
                //5
                //6
                //7
                //8
                //9
                //10
                //11

            }

            return doc;
        }

    }
}

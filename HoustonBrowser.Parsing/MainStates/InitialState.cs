using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.HTML;

namespace HoustonBrowser.Parsing
{
    public class InitialState//:State
    {
        Stack<Node> openedTags;
        delegate void TagProcessing(Node currentNode);
        private Dictionary<Token,TagProcessing> processedTags;
        public InitialState(Stack<Node> openTags)
        {
            openedTags = openTags;
            processedTags.Add(new Token((int)TokenType.NameOfTag,"html"),HTMLOpenProcessing);
        }
        public void ProcessToken(Token token,Node currentNode)
        {
            if(processedTags.ContainsKey(token))
            {
                processedTags.GetValueOrDefault(token).Invoke(currentNode);
            }
            else
            {
                Console.WriteLine("Some structure problems in your html page html/head/body");
            }
        }
        private void HTMLOpenProcessing(Node currentNode)
        {
            currentNode = new HTMLDocument();                                                    
            var item = new Element("html");
            currentNode.AppendChild(item);
            openedTags.Push(item);
            processedTags.Remove(new Token((int)TokenType.NameOfTag,"html"));
        }
        private void HEADOpenProcessing(Node currentNode)
        {

        }
        private void BODYOpenProcessing(Node currentNode)
        {

        }
        private void HTMLCloseProcessing(Node currentNode)
        {

        }
        private void HEADCloseProcessing(Node currentNode)
        {

        }
        private void BODYCloseProcessing(Node currentNode)
        {

        }
    }
}
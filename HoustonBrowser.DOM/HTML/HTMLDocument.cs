using System;
using System.Collections.Generic;
using HoustonBrowser.DOM.Core;
using HoustonBrowser.DOM.HTML.Interface;

namespace HoustonBrowser.DOM.HTML
{
    public class HTMLDocument: Document, IHTMLDocument
    {
        public string Title {get; set;}
        
        public HTMLDocument(): base() {}


        public void Open() {}
        public void Close() {}

        public void Write(string text) {}

        public void Writeln(string text) {}

        public Node[] GetElementByName(string elementName) 
        {
            return null;
        }
    }
}
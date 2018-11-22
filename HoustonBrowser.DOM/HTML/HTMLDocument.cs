using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLDocument: Document
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
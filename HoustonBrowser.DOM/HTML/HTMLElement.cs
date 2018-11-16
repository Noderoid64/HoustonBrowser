using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLElement: Element
    {
        public string Id {get; set;}
        public string Title {get; set;}
        public string Lang {get; set;}
        public string Dir {get; set;}
        public string ClassName {get; set;}

        public HTMLElement(string tagName): base(tagName) {}   
    }
}
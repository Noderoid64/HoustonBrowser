using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLHeadingElement : Element
    {
        public string Alighn { get; set; }
        public HTMLHeadingElement(string tagName): base(tagName) {}   
    }
}
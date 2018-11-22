using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLHtmlElement : Element
    {
        public string Version { get; set; }
        public HTMLHtmlElement() : base("html") { }
    }
}
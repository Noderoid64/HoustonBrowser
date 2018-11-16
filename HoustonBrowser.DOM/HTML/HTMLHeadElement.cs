using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLHeadElement : Element
    {
        public string Profile { get; set; }
        public HTMLHeadElement() : base("head") { }
    }
}
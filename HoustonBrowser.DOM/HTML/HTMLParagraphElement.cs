using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLParagraphElement : Element
    {
        public string Alighn { get; set; }
        public HTMLParagraphElement() : base("p") { }
    }
}
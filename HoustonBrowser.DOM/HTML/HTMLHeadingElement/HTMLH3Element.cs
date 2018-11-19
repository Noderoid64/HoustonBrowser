using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLH3Element : Element
    {
        public string Alighn { get; set; }
        public HTMLH3Element(): base("h3") {}   
    }
}
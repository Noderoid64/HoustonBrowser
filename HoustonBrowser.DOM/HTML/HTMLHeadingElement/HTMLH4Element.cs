using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLH4Element : Element
    {
        public string Alighn { get; set; }
        public HTMLH4Element(): base("h4") {}   
    }
}
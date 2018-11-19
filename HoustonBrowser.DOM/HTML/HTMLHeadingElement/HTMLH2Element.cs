using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLH2Element : Element
    {
        public string Alighn { get; set; }
        public HTMLH2Element(): base("h2") {}   
    }
}
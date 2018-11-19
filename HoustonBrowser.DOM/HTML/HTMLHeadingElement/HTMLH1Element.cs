using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLH1Element : Element
    {
        public string Alighn { get; set; }
        public HTMLH1Element(): base("h1") {}   
    }
}
using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLH5Element : Element
    {
        public string Alighn { get; set; }
        public HTMLH5Element(): base("h5") {}   
    }
}
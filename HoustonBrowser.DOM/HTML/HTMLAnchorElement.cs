using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.DOM
{
    class HTMLAnchorElement: Element
    {
        public string Href { get; set; }

        public HTMLAnchorElement() : base("a") { }
    }
}


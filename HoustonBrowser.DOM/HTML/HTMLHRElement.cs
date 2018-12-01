using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.DOM
{
    class HTMLHRElement : Element
    {
        public string Src { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public HTMLHRElement() : base("hr") { }
    }
}

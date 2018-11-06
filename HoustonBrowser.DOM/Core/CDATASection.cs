using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class CDATASection : Text
    {
        public CDATASection(string data):
            base(TypeOfNode.CDATA_SECTION_NODE, "#cdata-section", data)
        { }
    }
}
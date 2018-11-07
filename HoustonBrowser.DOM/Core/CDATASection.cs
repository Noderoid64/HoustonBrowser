using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class CDATASection : Text
    {
        public CDATASection(string data):
            base(TypeOfNode.CDATA_SECTION_NODE, "#cdata-section", data)
        { }
    }
}
using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class DocumentFragment  : Node
    {
        public DocumentFragment() :
            base(TypeOfNode.DOCUMENT_FRAGMENT_NODE, "#document-fragment", null)
        { }
    }
}
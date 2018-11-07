using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class DocumentFragment  : Node
    {
        public DocumentFragment() :
            base(TypeOfNode.DOCUMENT_FRAGMENT_NODE, "#document-fragment", null)
        { }
    }
}
using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class DocumentType : Node
    {
        readonly NamedNodeMap entities;
        readonly NamedNodeMap notations;

        public string Name { get => nodeName; }
        public DocumentType(string name) :
            base(TypeOfNode.DOCUMENT_TYPE_NODE, name, null)
        { }

    }
}
using System;

namespace HoustonBrowser.DOM.Core.Interface
{
    public interface INode
    {
        Node InsertBefore(Node newChild, Node refChild);
        Node ReplaceChild(Node newChild, Node oldChild);
        Node RemoveChild(Node oldChild);

        Node AppendChild(Node newChild);
        bool HasChildNodes();
        Node CloneNode(bool deep);
    }
}
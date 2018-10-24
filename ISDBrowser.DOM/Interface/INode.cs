using System;

namespace ISDBrowser.DOM.Interface
{
    public interface INode
    {
        Node insertBefore(Node newChild, Node refChild);
        Node replaceChild(Node newChild, Node oldChild);
        Node removeChild(Node oldChild);

        Node appendChild(Node newChild);
        bool hasChildNodes();
        Node cloneNode(bool deep);
    }
}
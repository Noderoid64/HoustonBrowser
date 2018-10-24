using System;
using System.Text;
using System.Collections.Generic;
using ISDBrowser.DOM.Interface;

namespace ISDBrowser.DOM
{
    public class Node : INode
    {
        enum NodeType
        {
            ELEMENT_NODE = 1,
            ATTRIBUTE_NODE,
            TEXT_NODE,
            CDATA_SECTION_NODE,
            ENTITY_REFERENCE_NODE,
            ENTITY_NODE,
            PROCESSING_INSTRUCTION_NODE,
            COMMENT_NODE,
            DOCUMENT_NODE,
            DOCUMENT_TYPE_NODE,
            DOCUMENT_FRAGMENT_NODE,
            NOTATION_NODE
        }

        string nodeName;   // Узнать про кодировку
        string nodeValue;
        NodeType nodeType;
        Node parentNode;
        List<Node> childNodes;
        Node firstChild;
        Node lastChild;
        Node previousSibling;
        Node nextSibling;
        // readonly NamedNodeMap attributes; // предоставляет поиск узлоа во имени
        // readonly Document ownerDocument;

        public Node(Node parentNode)
        {
            this.parentNode = parentNode;
            childNodes = new List<Node>();
        }
        public Node insertBefore(Node newChild, Node refChild)
        {
            if (childNodes.Contains(refChild))
            {
                int index = childNodes.IndexOf(refChild);
                childNodes.Insert(index, newChild);
            }

            return newChild;
        }
        public Node replaceChild(Node newChild, Node oldChild)
        {
            if (childNodes.Contains(oldChild))
            {
                int index = childNodes.IndexOf(oldChild);
                childNodes.Remove(oldChild);
                childNodes.Insert(index, newChild);
            }

            return newChild;
        }
        public Node removeChild(Node oldChild)
        {
            if (childNodes.Contains(oldChild))
            {
                childNodes.Remove(oldChild);
            }
            return oldChild;
        }
        public Node appendChild(Node newChild)
        {
            childNodes.Add(newChild);
            return newChild;
        }
        public bool hasChildNodes()
        {
            if (childNodes.Count == 0)
                return true;
            else
                return false;
        }
        public Node cloneNode(bool deep)
        {
            if (deep)
            {
                if (childNodes.Count != 0)
                {
                    foreach (var node in childNodes)
                    {
                        return node.cloneNode(deep);
                    }
                }
                else
                {
                    
                }
            }
            
            return (Node) this.MemberwiseClone();
        }
    }
}
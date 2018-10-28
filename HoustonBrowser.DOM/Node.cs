using System;
using System.Text;
using System.Collections.Generic;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Node : INode
    {
        public enum TypeOfNode
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

        string nodeName;
        string nodeValue;
        TypeOfNode nodeType;
        Node parentNode;
        List<Node> childNodes;
        Node firstChild;
        Node lastChild;
        Node previousSibling;
        Node nextSibling;

        protected NamedNodeMap attributes;
        readonly Document ownerDocument;

        public string NodeName { get => nodeName; }
        public string NodeValue { get => nodeValue; }
        public TypeOfNode NodeType { get => nodeType; }
        public Node ParentNode { get => parentNode; }
        public List<Node> ChildNodes { get => childNodes; }
        public Node FirstChild { get => firstChild; }
        public Node LastChild { get => lastChild; }
        public Node PreviousSibling { get => previousSibling; }
        public Node NextSibling { get => nextSibling; }
        public NamedNodeMap Attributes { get => attributes; }
        public Document OwnerDocument { get => ownerDocument; }

        public Node() { }

        public Node(TypeOfNode nodeType, string nodeName, string nodeValue)
        {
            this.nodeType = nodeType;
            this.nodeName = nodeName;
            this.nodeValue = nodeValue;
        }

        public Node(Node parentNode)
        {
            this.parentNode = parentNode;
            childNodes = new List<Node>();
        }

        public Node InsertBefore(Node newChild, Node refChild)
        {
            if (childNodes.Contains(refChild))
            {
                int index = childNodes.IndexOf(refChild);
                childNodes.Insert(index, newChild);
            }

            return newChild;
        }

        public Node ReplaceChild(Node newChild, Node oldChild)
        {
            if (childNodes.Contains(oldChild))
            {
                int index = childNodes.IndexOf(oldChild);
                childNodes.Remove(oldChild);
                childNodes.Insert(index, newChild);
            }

            return newChild;
        }
        public Node RemoveChild(Node oldChild)
        {
            if (childNodes.Contains(oldChild))
            {
                childNodes.Remove(oldChild);
            }
            return oldChild;
        }

        public Node AppendChild(Node newChild)
        {
            childNodes.Add(newChild);
            return newChild;
        }

        public bool HasChildNodes()
        {
            if (childNodes.Count == 0)
                return true;
            else
                return false;
        }

        public Node CloneNode(bool deep)
        {
            if (deep)
            {
                if (childNodes.Count != 0)
                {
                    foreach (var node in childNodes)
                    {
                        return node.CloneNode(deep);
                    }
                }
                else
                {

                }
            }

            return (Node)this.MemberwiseClone();
        }
    }
}
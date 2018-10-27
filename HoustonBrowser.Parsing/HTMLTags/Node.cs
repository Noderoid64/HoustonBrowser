using System;
using System.Collections.Generic;

namespace Houston.DOM
{
    public class Node
    {
        int nodeType;
        string nodeName;
        Node parentNode;
        List<Node> childNodes;
        Node firstChild;
        Node lastChild;
        Node previousSibling;
        Node nextSibling;
        //readonly NamedNodeMap attributes;
        //readonly Document ownerDocument;
        string nodeValue;
        public int NodeType { get => nodeType; }
        public string NodeName { get => nodeName; }
        public Node ParentNode { get => parentNode;}
        public List<Node> ChildNodes { get => childNodes;}
        public Node FirstChild { get => firstChild;}
        public Node LastChild { get => lastChild;}
        public Node PreviousSibling { get => previousSibling;}

        public Node()
        {

        }
        Node insertBefore(Node newChild,Node refChild)
        {
            if(refChild == null)
            {
                childNodes.Add(newChild);
            }
            else
            {
                if(childNodes.IndexOf(refChild)-1 >= 0)
                    childNodes.Insert(childNodes.IndexOf(refChild)-1,newChild);
                else
                {
                    Node temporary = new Node();
                    temporary.cloneNode(true);//ПЕРЕСМОТРЕТЬ
                    
                }
            }
            return newChild;
        }
        //Node replaceChild(in Node newChild, in Node oldChild){}
        //Node removeChild(in Node oldChild){}
        //Node appendChild(in Node newChild){}
        //boolean hasChildNodes(){}
        Node cloneNode(bool deep)
        {
            return new Node();
        }
    }
}
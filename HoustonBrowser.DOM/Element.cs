using System;
using System.Collections.Generic;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Element: Node, IElement
    {
        public string TagName { get => nodeName; }

        public Element(string tagName) :
                    base(TypeOfNode.ELEMENT_NODE, tagName, null)
        { }

        public string GetAttribute(string name)
        {
            var attr = (Attr)attributes.GetNamedItem(name);
            return attr.Value;
        }

        public void SetAttribute(string name, string value)
        {
            Attr attr = new Attr(name);
            attr.Value = value;

            attributes.SetNamedItem((Node)attr);
        }

        public void RemoveAttribute(string name)
        {
            attributes.RemoveNamedItem(name);
        }

        public Attr GetAttributeNode(string name)
        {
            return (Attr)attributes.GetNamedItem(name);
        }

        public Attr SetAttributeNode(Attr newAttr)
        {
            return (Attr)attributes.SetNamedItem((Node)newAttr);
        }

        public Attr RemoveAttributeNode(Attr oldAttr)
        {
            return (Attr) attributes.RemoveNamedItem(oldAttr.Name); 
        }

        public List<Node> GetElementsByTagName(string name)
        {
            var list = new List<Node>();

            foreach(var node in ChildNodes)
            {
                if(node.NodeType == TypeOfNode.ELEMENT_NODE)
                    if(node.NodeName == name || name == "*")
                        list.Add(node);
            }

            return list;
        }
    }
}
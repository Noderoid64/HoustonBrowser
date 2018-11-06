using System;
using System.Collections.Generic;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class MockDocument : Node, IDocument
    {
        public MockDocument() :
            base(TypeOfNode.DOCUMENT_NODE, "#document", null)
        { }

// *******************************************
        public string DomWork()
        {
            return "DOM Works";
        }
//***********
        public Element CreateElement(string tagName)
        {
            return new Element(tagName);
        }

        public DocumentFragment CreateDocumentFragment()
        {
            return new DocumentFragment();
        }

        public Text CreateTextNode(string data)
        {
            return new Text(data);
        }

        public Comment CreateComment(string data)
        {
            return new Comment(data);
        }

        public CDATASection CreateCDATASection(string data)
        {
            return new CDATASection(data);
        }
        public ProcessingInstruction CreateProcessingInstruction(string target, string data)
        {
            return new ProcessingInstruction(target, data);
        }

        public Attr CreateAttribute(string name)
        {
            return new Attr(name);
        }

        public EntityReference CreateEntityReference(string name)
        {
            return new EntityReference(name);
        }

        public List<Node> GetElementsByTagName(string name)
        {
            var list = new List<Node>();

            foreach (var node in ChildNodes)
            {
                if (node.NodeType == TypeOfNode.ELEMENT_NODE)
                    if (node.NodeName == name || name == "*")
                        list.Add(node);
            }

            return list;
        }
    }
}
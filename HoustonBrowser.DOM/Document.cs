using System;
using System.Collections.Generic;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Document: Node, IDocument
    {
        public Document():
            base(TypeOfNode.DOCUMENT_NODE, "#document", null) { }

        public Element CreateElement(string tagName) 
        {
            return new Element(tagName);
        }
                                          
        // DocumentFragment createDocumentFragment();
        // Text createTextNode(string data);
        // Comment createComment(string data);
        // CDATASection createCDATASection(string data);                          
        // ProcessingInstruction createProcessingInstruction(string target, string data);
                                                        
        public Attr CreateAttribute(string name)
        {
            return new Attr(name);
        }
                                            
        // EntityReference createEntityReference(string name);
                                                  
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
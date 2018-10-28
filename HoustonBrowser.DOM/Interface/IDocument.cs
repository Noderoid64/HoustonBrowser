using System;
using System.Collections.Generic;

namespace HoustonBrowser.DOM.Interface
{
    public interface IDocument : INode
    {
        Element CreateElement(string tagName);
                                          
        // DocumentFragment createDocumentFragment();
        // Text createTextNode(string data);
        // Comment createComment(string data);
        // CDATASection createCDATASection(string data);                          
        // ProcessingInstruction createProcessingInstruction(string target, string data);
                                                        
        Attr CreateAttribute(string name);
                                            
        // EntityReference createEntityReference(string name);
                                                  
        List<Node> GetElementsByTagName(string tagname);
    }
}
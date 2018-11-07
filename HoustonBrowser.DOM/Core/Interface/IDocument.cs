using System;
using System.Collections.Generic;

namespace HoustonBrowser.DOM.Core.Interface
{
    public interface IDocument : INode
    {
        Element CreateElement(string tagName);                         
        DocumentFragment CreateDocumentFragment();
        Text CreateTextNode(string data);
        Comment CreateComment(string data);
        CDATASection CreateCDATASection(string data);                          
        ProcessingInstruction CreateProcessingInstruction(string target, string data);
                                                        
        Attr CreateAttribute(string name);
                                            
        EntityReference CreateEntityReference(string name);
                                                  
        List<Node> GetElementsByTagName(string tagname);

        string DomWork();
    }
}
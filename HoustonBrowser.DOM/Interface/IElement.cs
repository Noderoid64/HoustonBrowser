using System;
using System.Collections.Generic;

namespace HoustonBrowser.DOM.Interface
{
    public interface IElement : INode
    {
        string GetAttribute(string name);
        void SetAttribute(string name, string value);
        void RemoveAttribute(string name);
        Attr GetAttributeNode(string name);
        Attr SetAttributeNode(Attr newAttr);
        Attr RemoveAttributeNode(Attr oldAttr);
        List<Node> GetElementsByTagName(string name);
        // void Normalize();
    }
}
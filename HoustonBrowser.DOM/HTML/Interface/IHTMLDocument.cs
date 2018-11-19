using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM.Interface
{
    interface IHTMLDocument: IHTMLElement, IDocument
    {
        void Open();
        void Close();
        void Write(string text);
        void Writeln(string text);
        Node[] GetElementByName(string elementName);
    }
}
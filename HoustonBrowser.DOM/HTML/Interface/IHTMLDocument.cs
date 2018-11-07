using System;
using System.Collections.Generic;
using HoustonBrowser.DOM.Core;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.HTML.Interface
{
    interface IHTMLDocument: IDocument
    {
        void Open();
        void Close();
        void Write(string text);
        void Writeln(string text);
        Node[] GetElementByName(string elementName);
    }
}
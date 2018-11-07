using HoustonBrowser.DOM.Core;
using System;

namespace HoustonBrowser.Parsing
{
    public interface IParser
    {
        string Parse();
        Document Parse(string value);
        event EventHandler<string> onNonHtmlEvent;
    }
}

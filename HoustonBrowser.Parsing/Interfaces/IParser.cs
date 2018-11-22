using HoustonBrowser.DOM;
using System;

namespace HoustonBrowser.Parsing
{
    public interface IParser
    {
        string Parse();
        HTMLDocument Parse(string value);
        event EventHandler<string> onNonHtmlEvent;
    }
}

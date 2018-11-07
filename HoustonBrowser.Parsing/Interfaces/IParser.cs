using HoustonBrowser.DOM.Core;

namespace HoustonBrowser.Parsing
{
    public interface IParser
    {
        string Parse();
        Document Parse(string value);
    }
}

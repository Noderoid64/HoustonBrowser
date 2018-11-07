using HoustonBrowser.DOM;

namespace HoustonBrowser.Parsing
{
    public interface IParser
    {
        string Parse();
        Document Parse(string value);
    }
}

using HoustonBrowser.DOM;

namespace HoustonBrowser.Parser
{
    public interface IParser
    {
        string Parse();
        Document Parse(string value);
    }
}

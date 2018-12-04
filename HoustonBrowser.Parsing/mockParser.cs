using System;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Parsing
{
    public class mockParser:IParser
    {
        public event EventHandler<string> onNonHtmlEvent;

        public string Parse()
        {
            return " Parser is alive! ";
        }

        public HTMLDocument Parse(string value)
        {
            return new HTMLDocument();
        }

        public HTMLDocument Parse(string value, HTMLDocument doc)
        {
            throw new NotImplementedException();
        }
    }
}
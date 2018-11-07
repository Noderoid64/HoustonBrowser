using System;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Parsing
{
    public class mockParser:IParser
    {

        public string Parse()
        {
            return " Parser is alive! ";
        }

        public Document Parse(string value)
        {
            return new Document();
        }
    }
}
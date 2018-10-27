using System;
using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Parsers{
    internal class HttpDatagramParser : IHttpDatagramParser
    {
        public string Parse(HttpDatagram datagram)
        {
            if(datagram is IParseable parseable)
            return parseable.GetString();
            else
            throw new Exception("HttpDatagram is not parseble");
        }
    }
}
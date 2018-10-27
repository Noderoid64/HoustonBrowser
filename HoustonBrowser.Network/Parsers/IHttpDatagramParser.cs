using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Parsers{
    internal interface IHttpDatagramParser{
        string Parse(HttpDatagram datagram);
    }
}
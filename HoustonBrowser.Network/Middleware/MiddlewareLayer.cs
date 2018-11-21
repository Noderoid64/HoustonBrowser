using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Middleware{
    internal abstract class MiddlewareLayer{
    public abstract HttpResponseDatagram Handle(HttpResponseDatagram datagram);
    }
}
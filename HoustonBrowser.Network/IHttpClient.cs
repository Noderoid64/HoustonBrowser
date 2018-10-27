using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("HoustonBrowser.Network.Test")]

namespace HoustonBrowser.HttpModule{
    public interface IHttpClient
    {
        /*
        OPTIONS,
        GET,
        HEAD,
        POST,
        PUT,
        PATCH,
        DELETE,
        TRACE,
        CONNECT
         */
        string GET(string host);
        // string POST(string host, string body);
        // string PUT(string host, string body);
        // string DELETE(string host, string body);
    }
}
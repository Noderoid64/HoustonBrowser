using HoustonBrowser.HttpModule.Builders;
using HoustonBrowser.HttpModule.Parsers;
using HoustonBrowser.HttpModule.Model;
using System.Text;

namespace HoustonBrowser.HttpModule
{
    public class HttpClient : IHttpClient
    {
        public string GetCss(string host)
        {
            throw new System.NotImplementedException();
        }

        public string GetHtml(string host)
        {
            string uri = UrlBuilder.GetRequestUri(host);
            host = UrlBuilder.GetHost(host);
            
            HttpDatagramBuilder httpDatagramBuilder = new HttpDatagramBuilder();
            httpDatagramBuilder.AddStart(HttpMethods.GET, uri, new HttpVersion(1, 1));
            httpDatagramBuilder
            .AddHeader(new HeaderFieldHost(host))
            .AddHeader(new HeaderFieldCacheControl(HeaderFieldCacheControl.Params.NoChache))
            .AddHeader(new HeaderFieldAccept(HeaderFieldAccept.Type.text, HeaderFieldAccept.SubType.html, 1));
            if (httpDatagramBuilder.isReady())
            {
                HttpSender sender = new HttpSender();
                IHttpDatagramParser parser = new HttpDatagramParser();
                string request = parser.Parse(httpDatagramBuilder.Build());
                string response = sender.SendHttp(host, request);
                return response;
            }

            return null;
        }

        public string GetStatus()
        {
            return "HttpClient works";
        }
    }
}
using HoustonBrowser.HttpModule.Builders;
using HoustonBrowser.HttpModule.Parsers;
using HoustonBrowser.HttpModule.Model;
using System.Text;

namespace HoustonBrowser.HttpModule
{
    public class HttpClient : IHttpClient
    {
        public string GET(string host)
        {
            //http://www.netside.net/boba/webmasters.html
            string[] local = host.Split('/');
            host = local[0] + "//" + local[2];
            string url = "";
            for (int i = 3; i < local.Length; i++)
            {
                url += "/" + local[i];
            }
            
            HttpDatagramBuilder httpDatagramBuilder = new HttpDatagramBuilder();
            httpDatagramBuilder.AddStart(HttpMethods.GET, url, new HttpVersion(1, 1));
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
    }
}
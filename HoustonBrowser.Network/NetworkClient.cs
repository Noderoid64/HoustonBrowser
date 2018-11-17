using HoustonBrowser.HttpModule.Senders;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;
using HoustonBrowser.HttpModule.Builders;

namespace HoustonBrowser.HttpModule
{
    public class NetworkClient : INetworkClient
    {
        public string Get(string host)
        {
            ISender sender;
            if (host.StartsWith("https"))
                sender = new HttpsSender();
            else
                sender = new HttpSender();


            HttpDatagram datagram = new HttpRequestDatagram(HttpMethods.GET, UrlBuilder.GetRequestUri(host), HttpVersion.Get11());
            datagram.header.AddHeaderField(new HttpHeaderField("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"));
            datagram.header.AddHeaderField(new HttpHeaderField("Host: " + UrlBuilder.GetHost(host)));
            datagram.header.AddHeaderField(new HttpHeaderField("Accept-Encoding: gzip, deflate"));
            datagram.header.AddHeaderField(new HttpHeaderField("Accept-Language: en-US,en;q=0.9,ru;q=0.8"));



            return sender.Send(UrlBuilder.GetHost(host), datagram.GetString());
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}
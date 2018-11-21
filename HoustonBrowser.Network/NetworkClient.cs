using HoustonBrowser.HttpModule.Senders;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;
using HoustonBrowser.HttpModule.Builders;
using HoustonBrowser.HttpModule.Middleware;

using System.Text;
using System;

namespace HoustonBrowser.HttpModule
{
    public class NetworkClient : INetworkClient
    {
        MiddlewareLayer contentTypeLayer;
        public NetworkClient()
        {
            contentTypeLayer = new ContentTypeLayer();
        }
        public string Get(string host)
        {
            ISender sender;
            if (host.StartsWith("https"))
                sender = new HttpsSender();
            else
                sender = new HttpSender();


            HttpRequestDatagram datagram = HttpDatagramBuilder.GetRequestDatagram(host); // Получаем базовую модель датаграммы (без алгоритмов сжатия)

            string response = sender.Send(UrlBuilder.GetHost(host), datagram.GetString());
            HttpResponseDatagram dat = new HttpResponseDatagram(response);

            contentTypeLayer.Handle(dat);

            return dat.body.GetString();
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}
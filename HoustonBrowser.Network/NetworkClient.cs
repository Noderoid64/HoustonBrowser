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
        MiddlewareLayer LocationLayer;
        public NetworkClient()
        {
            contentTypeLayer = new ContentTypeLayer();
            LocationLayer = new LocationLayer();
        }
        public string Get(string host)
        {
            HttpResponseDatagram dat = GetDatagram(host);

            return dat.body.GetString();
        }

        internal HttpResponseDatagram GetDatagram(string host)
        {
            ISender sender;
            if (host.StartsWith("https"))
                sender = new HttpsSender();
            else
                sender = new HttpSender();


            HttpRequestDatagram datagram = HttpDatagramBuilder.GetRequestDatagram(host); // Получаем базовую модель датаграммы (без алгоритмов сжатия)

            string response = sender.Send(UrlBuilder.GetHost(host), datagram.GetString());

            HttpResponseDatagram responseDatagram = new HttpResponseDatagram(response);

            contentTypeLayer.Handle(responseDatagram);
            responseDatagram = LocationLayer.Handle(responseDatagram);


            return responseDatagram;
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}
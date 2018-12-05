using HoustonBrowser.HttpModule.Senders;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;
using HoustonBrowser.HttpModule.Builders;
using HoustonBrowser.HttpModule.Middleware;

using System.Text;
using System;
using System.Threading.Tasks;
using System.Threading;

using MercuryLogger;

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
        internal HttpResponseDatagram GetDatagram(string host, CancellationToken token = default(CancellationToken))
        {
            Logger logger = MainLogger.GetInstance();

            ISender sender;
            if (host.StartsWith("https"))
            {
                sender = new HttpsSender();
                logger.Log("[Network] create https sender");
            }
            else
            {
                sender = new HttpSender();
                logger.Log("[Network] create http sender");
            }


            //Формируем запрос
            logger.Log("[Network] Creating request");
            HttpRequestDatagram datagram = HttpDatagramBuilder.GetRequestDatagram(host); // Получаем базовую модель датаграммы (без алгоритмов сжатия)
            if (token != CancellationToken.None)
                token.ThrowIfCancellationRequested();
                logger.Log("[Network] Creating request done");
            //Запрашиваем 
            logger.Log("[Network] Send request");
            string response = sender.Send(UrlBuilder.GetHost(host), datagram.GetString());
            if (token != CancellationToken.None)
                token.ThrowIfCancellationRequested();
                logger.Log("[Network] Send request done");
            //Обрабатываем
            logger.Log("[Network] Parsing response");
            HttpResponseDatagram responseDatagram = new HttpResponseDatagram(response);
            logger.Log("[Network] Parsing response done");

            contentTypeLayer.Handle(responseDatagram);
            responseDatagram = LocationLayer.Handle(responseDatagram);
            if (token != CancellationToken.None)
                token.ThrowIfCancellationRequested();

            return responseDatagram;
        }
        public string GetStatus()
        {
            return "HttpModule is working";
        }
        public Task<string> GetTask(string host)
        {

            Task<string> t = new Task<string>(() => Get(host));
            MainLogger.GetInstance().Log("[Network] Start new Task on {" + host + "}");
            t.Start();
            return t;
        }
        public Task<string> GetTask(string host, CancellationToken token)
        {
            Task<string> t = new Task<string>(() =>
            {
                return GetDatagram(host, token).body.GetString();
            }, token);
            t.Start();
            return t;
        }
    }
}
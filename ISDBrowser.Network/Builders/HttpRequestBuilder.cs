using System;
using HttpModule.Model;

namespace HttpModule
{
    internal class HttpRequestBuilder
    {
        private HttpDatagram datagram;
        public HttpRequestBuilder AddStartRequest(HttpMethods method, string uri, HttpVersion version)
        {
            datagram = new HttpRequestDatagram(method, uri, version);
            return this;
        }
        public HttpRequestBuilder AddHeaderHost(string host)
        {
            return this;
        }
        public HttpRequestBuilder AddHeaderAccept()
        {
            return this;
        }

        public HttpDatagram Build()
        {
            //Добавить проверку на готовность билда
            return datagram;
        }
    }
}
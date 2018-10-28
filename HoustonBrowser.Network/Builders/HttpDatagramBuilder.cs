using System;
using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Builders
{
    internal class HttpDatagramBuilder
    {
        private HttpDatagram datagram;
        public void AddStart(HttpMethods method, string url, HttpVersion version)
        {
            if (datagram == null)
                datagram = new HttpRequestDatagram(method, url, version);
            else
            {
                if (datagram is HttpRequestDatagram dtg)
                {
                    dtg.Method = method;
                    dtg.Url = url;
                    dtg.Version = version;
                }
            }
        }
        public void AddStart(ushort statusCode, string phrase, HttpVersion version)
        {
            if (datagram == null)
                datagram = new HttpResponseDatagram(statusCode, phrase, version);
            else
            {
                if (datagram is HttpResponseDatagram dtg)
                {
                    dtg.StatusCode = statusCode;
                    dtg.ReasonPhrase = phrase;
                    dtg.Version = version;
                }
            }
        }
        public HttpDatagramBuilder AddHeader(HttpHeaderField field)
        {
            if(datagram?.header == null)
            datagram.header = new HttpHeader();
            datagram?.header?.AddHeaderField(field);
            return this;
        }
        public void AddBody(HttpBody body)
        {

        }
        public bool isReady()
        {
            return true;
        }
        public HttpDatagram Build()
        {
            return datagram;
        }
    }
}
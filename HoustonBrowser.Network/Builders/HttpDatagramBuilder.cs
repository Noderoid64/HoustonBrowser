using System;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule;

namespace HoustonBrowser.HttpModule.Builders
{
    internal class HttpDatagramBuilder
    {
        private HttpDatagram datagram;

        #region AddComponents
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
            if (datagram?.header == null)
                datagram.header = new HttpHeader();
            datagram?.header?.AddHeaderField(field);
            return this;
        }
        public void AddBody(HttpBody body)
        {
            datagram.body = body;
        }
        #endregion

        public static HttpRequestDatagram GetRequestDatagram(string host)
        {
            HttpRequestDatagram datagram = new HttpRequestDatagram(HttpMethods.GET, UrlBuilder.GetRequestUri(host), HttpVersion.Get11());
            datagram.header.AddHeaderField(new HttpHeaderField("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"));
            datagram.header.AddHeaderField(new HttpHeaderField("Host: " + UrlBuilder.GetHost(host)));
            //datagram.header.AddHeaderField(new HttpHeaderField("Accept-Encoding: gzip, deflate"));
            datagram.header.AddHeaderField(new HttpHeaderField("Accept-Language: en-US,en;q=0.9,ru;q=0.8")); //Content-Type: text/html; charset=utf-8
            return datagram;
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
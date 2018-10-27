using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
   internal class HttpRequestDatagram : HttpDatagram
    {

        public HttpMethods Method { get; set; }
        public string Url { get; set; }

        public HttpRequestDatagram(HttpMethods method, string url, HttpVersion version) : base(version)
        {
            this.Method = method;
            this.Url = url;
        }

        public override bool isRequest()
        {
            return true;
        }

        public override bool isValidStart()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBytes(Encoding encoder)
        {
            return encoder.GetBytes(Method.ToString() + " " + Url)
                .Concat(Version.GetBytes(encoder)).ToArray()
                .Concat(encoder.GetBytes("\r\n")).ToArray()
                .Concat(header.GetBytes(encoder)).ToArray()
                .Concat(body.GetBytes(encoder)).ToArray();
        }

        public override string GetString()
        {
            return Method.ToString() + " " + Url + " " + Version.GetString() + "\r\n" + header.GetString() + body?.GetString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model
{
    class HttpRequestDatagram : HttpDatagram
    {

        public HttpMethods Method { get; private set; }
        public string Uri { get; private set; }

        public HttpRequestDatagram(HttpMethods method, string uri, HttpVersion version) : base(version)
        {
            this.Method = method;
            this.Uri = uri;
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
            return encoder.GetBytes(Method.ToString() + " " + Uri)
                .Concat(Version.GetBytes(encoder)).ToArray()
                .Concat(encoder.GetBytes("\r\n")).ToArray()
                .Concat(header.GetBytes(encoder)).ToArray()
                .Concat(body.GetBytes(encoder)).ToArray();
        }

        public override string GetString()
        {
            return Method.ToString() + " " + Uri + " " + Version.GetString() + "\r\n" + header.GetString() + body?.GetString();
        }
    }
}

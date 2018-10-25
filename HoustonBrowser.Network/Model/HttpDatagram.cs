using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal abstract class HttpDatagram : IParseable
    {
        public HttpVersion Version { get; protected set; }
        public HttpHeader header { get; set; }
        public HttpBody body { get; set; }

        public HttpDatagram(HttpVersion version)
        {
            Version = version;
        }

        public abstract bool isValidStart();
        public abstract bool isRequest();

        public abstract byte[] GetBytes(Encoding encoder);

        public abstract string GetString();

        public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }

        public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }
    }
}

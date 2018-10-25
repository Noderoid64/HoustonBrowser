using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModule.Model
{
  internal  class HttpResponseDatagram : HttpDatagram
    {
        public HttpResponseDatagram(ushort statusCode, string ReasonPhrase, HttpVersion version) : base(version)
        {

        }

        public ushort StatusCode { get; private set; }
        public string ReasonPhrase { get; private set; }

        public override byte[] GetBytes(Encoding encoder)
        {
            throw new NotImplementedException();
        }

        public override string GetString()
        {
            throw new NotImplementedException();
        }

        public override bool isRequest()
        {
            return false;
        }

        public override bool isValidStart()
        {
            throw new NotImplementedException();
        }
    }
}

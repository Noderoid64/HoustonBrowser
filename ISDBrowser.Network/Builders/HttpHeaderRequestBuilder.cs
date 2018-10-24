using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HttpModule.Model;
using HttpModule.Model.Headers;

namespace Hello.Http.Builders
{
    public class HttpHeaderRequestBuilder
    {
        private HttpHeader header;
        public HttpHeaderRequestBuilder(HttpHeader header)
        {
            this.header = header;
        }
        public HttpHeaderRequestBuilder AddHost(string host)
        {
            header.SetField(new HttpHeaderField(RequestHeader.Host, new AcceptValue(new AcceptContentString(host))));
            return this;
        }
    }
}

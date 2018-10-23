using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model.Headers.RequestFields
{
    public class HeaderAcceptField : HttpHeaderField
    {
        enum Types
        {
            application,
            audio,
            font,
            example,
            image,
            message,
            model,
            multypart,
            text,
            video
        }
        enum SubTypes
        {

        }

        public HeaderAcceptField(Type type): base(RequestHeader.Accept, null)
        {

        }
        public HeaderAcceptField(IEnumerable<AcceptValue> values) : base(RequestHeader.Accept, values)
        {
        }

        public HeaderAcceptField(AcceptValue value) : base(RequestHeader.Accept, value)
        {
        }
    }
}

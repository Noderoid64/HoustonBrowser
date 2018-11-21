using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;
using System.Text;
using System;

namespace HoustonBrowser.HttpModule.Middleware
{
    internal class ContentTypeLayer : MiddlewareLayer
    {
        const string defaultCoding = "ISO-8859-1";

        public override HttpResponseDatagram Handle(HttpResponseDatagram datagram)
        {
            string newCoding = null;
            Encoding encoderIn = Encoding.GetEncoding(defaultCoding);
            if (datagram.header.fields.Find(x => x.name == HeaderFieldContentType.FieldName) is HeaderFieldContentType a)
            {
                for (int i = 0; i < a.values.Length; i++)
                {
                    if (a.values[i].StartsWith("charset="))
                    {
                        newCoding = a.values[i].Substring("charset=".Length);
                        break;
                    }
                }
            }
            if (newCoding != null)
            {
                Encoding encoderOut = Encoding.GetEncoding(newCoding);
                byte[] data = encoderIn.GetBytes(datagram.body.GetString());
               // byte[] newData = Encoding.Convert(encoderIn, encoderOut, data);

                datagram.body.FromString(encoderOut.GetString(data, 0, data.Length));
            }


            return datagram;
        }
    }
}
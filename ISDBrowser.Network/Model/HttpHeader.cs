using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HttpModule.Model.Headers;

namespace HttpModule.Model
{
    public class HttpHeader : IParseable
    {
        public List<HttpHeaderField> Fields { get; private set; }

        public HttpHeader()
        {
            Fields = new List<HttpHeaderField>();
        }

        public void SetField(HttpHeaderField field)
        {
            if(Fields.FirstOrDefault(x=> x.Name == field.Name) is HttpHeaderField a)
            {
                a.AddValues(field.Values);
            }
            else
            {
                Fields.Add(field);
            }
        }

        public byte[] GetBytes(Encoding encoder)
        {
            byte[] bytes = new byte[0];
            for (int i = 0; i < Fields.Count; i++)
            {
                bytes = bytes.Concat(Fields[i].GetBytes(encoder)).ToArray();
                bytes = bytes.Concat(encoder.GetBytes("\r\n")).ToArray();
            }
            return bytes.Concat(encoder.GetBytes("\r\n")).ToArray();
        }

        public string GetString()
        {
            string localString = "";
            for (int i = 0; i < Fields.Count; i++)
            {
                localString += Fields[i].GetString() + "\r\n";
            }
            return localString + "\r\n";
        }

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

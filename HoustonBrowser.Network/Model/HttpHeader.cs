using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HoustonBrowser.HttpModule.Model.Headers;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpHeader : IParseable
    {
        private List<HttpHeaderField> fields;
        public HttpHeader()
        {
            fields = new List<HttpHeaderField>();
        }
        public void AddHeaderField(HttpHeaderField field)
        {
            if (fields.FirstOrDefault(x => x.name == field.name)!=null)
                throw new Exception("Header field already exist");
                else
                fields.Add(field);
        }

        #region IParseable
        public byte[] GetBytes(Encoding encoder)
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }

        public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }

        public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }


}

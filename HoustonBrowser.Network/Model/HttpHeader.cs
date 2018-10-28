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
            if (fields.FirstOrDefault(x => x.name == field.name) != null)
                throw new Exception("Header field already exist");
            else
                fields.Add(field);
        }



        #region IParseable
        public string GetString()
        {
            string localString = "";
            foreach (HttpHeaderField item in fields)
            {
                localString += item.GetString() + "\r\n";
            }
            return localString + "\r\n";
        }
        public void FromString(string value)
        {
            value = value.Substring(0, value.Length - 2);
            string[] localString = value.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var item in localString)
            {
                switch (item)
                {
                    case HeaderFieldHost.FieldName:
                        {
                            HeaderFieldHost field = new HeaderFieldHost(null);
                            field.FromString(item);
                            fields.Add(field);
                        }
                        break;
                    case HeaderFieldCacheControl.FieldName:
                        {
                            HeaderFieldCacheControl field = new HeaderFieldCacheControl(HeaderFieldCacheControl.Params.NoChache);
                            field.FromString(item);
                            fields.Add(field);
                        }
                        break;
                    case HeaderFieldAccept.FieldName:
                        {

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
        }

        #endregion
    }


}

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

        #region  HttpDatagram
        public override bool isValidStart()
        {
            throw new NotImplementedException();
        }

        public override bool isRequest()
        {
            return true;
        }

        #endregion

        #region IParseble
        public override string GetString()
        {
<<<<<<< HEAD
            string request = "";
            request += Method.ToString() + " ";
            request += (Url==null? "/" : Url) + " ";
            request += Version.GetString()+ "\r\n";
            request += header?.GetString();
            request += body?.GetString();
            return request;
=======
            return Method.ToString() + " " + Url + " " + Version.GetString() + "\r\n" + header?.GetString() + body?.GetString();
>>>>>>> [Update] realize IParseble
        }
        public override void FromString(string value)
        {

            int headerIndex = value.IndexOf("\r\n");
            int bodyIndex = value.IndexOf("\r\n\r\n");

            string startString = value.Substring(0, headerIndex + 2);
            string headerString = value.Substring(headerIndex + 2, bodyIndex - headerIndex);
            string bodyString = value.Substring(bodyIndex + 4);

            string[] startParam = startString.Split(' ');

            Method.FromString(startParam[0]);
            Url = startParam[1];
            Version.FromString(startParam[2].Substring(0,startParam.Length -2));

            header.FromString(headerString);
            body.FromString(bodyString);
            
        }
        #endregion
    }
}

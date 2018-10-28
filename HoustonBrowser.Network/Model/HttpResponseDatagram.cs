using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpResponseDatagram : HttpDatagram
    {
        public HttpResponseDatagram(ushort statusCode, string reasonPhrase, HttpVersion version) : base(version)
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
            this.Version = version;
        }

        public ushort StatusCode { get; set; }
        public string ReasonPhrase { get; set; }

        #region  IParseble
        public override string GetString()
        {
            return Version.ToString() + " " + StatusCode.ToString() + " " + ReasonPhrase + "\r\n" + header?.GetString() + body?.GetString();
        }
        public override void FromString(string value)
        {
            int headerIndex = value.IndexOf("\r\n");
            int bodyIndex = value.IndexOf("\r\n\r\n");

            string startString = value.Substring(0, headerIndex + 2);
            string headerString = value.Substring(headerIndex + 2, bodyIndex - headerIndex);
            string bodyString = value.Substring(bodyIndex + 4);

            string[] startParam = startString.Split(' ');

            Version.FromString(startParam[0]);
            StatusCode = ushort.Parse(startParam[1]);
            ReasonPhrase = (startParam[2].Substring(0, startParam.Length - 2));
            header.FromString(headerString);
            body.FromString(bodyString);
        }
        #endregion

        #region  HttpDatagram
        public override bool isRequest()
        {
            return false;
        }

        public override bool isValidStart()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}

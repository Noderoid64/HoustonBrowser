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
        public HttpResponseDatagram(string value) : base(null)
        {
            FromString(value);
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
            if (value == null)
            {
                FillNotFound();
                return;
            }

            int headerIndex = value.IndexOf("\r\n");
            int bodyIndex = value.IndexOf("\r\n\r\n");

            string startString = value.Substring(0, headerIndex + 2);
            string headerString = value.Substring(headerIndex + 2, bodyIndex - headerIndex);
            string bodyString = value.Substring(bodyIndex + 4);

            string[] startParam = startString.Split(' ');
            // startParam[0] - HTTP version
            // startparam[1] - status code
            // startparam[2] = phrase (useless)

            if (startParam.Length < 2)
                throw new Exception("Invalid number of start param");

            StatusCode = ushort.Parse(startParam[1]);

            if (startParam.Length > 2)
                ReasonPhrase = (startString.Substring(startString.IndexOf(startParam[2]))).Substring(0, startParam[2].Length - 2);
            /*сложные манипуляции со строками
            HTTP/1.1 OK Ok\r\n
            находим строку после ответа OK => Ok\r\n
            удаляем последние два символа переноса и возврата каретки
             */

            if (Version == null)
                Version = new HttpVersion();
            Version.FromString(startParam[0]);

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

        private void FillNotFound()
        {
            if (Version == null)
                Version = new HttpVersion();
                Version = HttpVersion.Get11();
                StatusCode = HttpStatusCode.NotFound;
                ReasonPhrase = "NotFound";
                body.FromString("<html><head>HoustonBrowser</head><body><h1>404 Not Found</h1></body></html>");

        }
    }
}

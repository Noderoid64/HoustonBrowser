using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal abstract class HttpDatagram : IParseable
    {
        public HttpVersion Version { get; set; }
        public HttpHeader header { get; set; }
        public HttpBody body { get; set; }

        public HttpDatagram(HttpVersion version)
        {
            Version = version;
            header = new HttpHeader();
<<<<<<< HEAD
<<<<<<< HEAD
            body = new HttpBody(null);
=======
            body = new HttpBody();
>>>>>>> [Update] realize IParseble
=======
            body = new HttpBody(null);
>>>>>>> [Fix] httpPageLoader
        }

        public abstract bool isValidStart();
        public abstract bool isRequest();

        #region IParseble
        public abstract string GetString();

        public abstract void FromString(string value);
        #endregion
    }
}

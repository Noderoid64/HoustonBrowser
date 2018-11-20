using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpVersion : IParseable
    {
        public uint Major { get; private set; }
        public uint Minor { get; private set; }
        public HttpVersion()
        {

        }
        public HttpVersion(uint major, uint minor)
        {
            this.Major = major;
            this.Minor = minor;
        }

        #region IParseble
        public string GetString()
        {
            return "HTTP/" + Major.ToString() + "." + Minor.ToString();
        }
        public void FromString(string value)
        {
            value = value.Substring(5);
            string[] localString = value.Split('.');
            Major = uint.Parse(localString[0]);
            Minor = uint.Parse(localString[1]);
        }
        #endregion

        public static HttpVersion Get11() => new HttpVersion(1, 1);
        public static HttpVersion Get10() => new HttpVersion(1, 0);
    }
}

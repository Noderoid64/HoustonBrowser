using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModule.Model
{
    internal class HttpVersion : IParseable
    {
        public uint Major { get; private set; }
        public uint Minor { get; private set; }

        public HttpVersion(uint major, uint minor)
        {
            this.Major = major;
            this.Minor = minor;
        }

        public byte[] GetBytes(Encoding encoder)
        {
            return encoder.GetBytes(GetString());
        }

        public string GetString()
        {
            return "HTTP/" + Major.ToString() + "." + Minor.ToString();
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

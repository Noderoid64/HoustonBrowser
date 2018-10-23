using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model
{
    public class HttpBody : IParseable
    {
        public byte[] GetBytes(Encoding encoder)
        {
            return null;
        }

        public string GetString()
        {
            return null;
        }

        public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }

        public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }
    }
}

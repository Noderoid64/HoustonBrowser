using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModule.Model
{
    internal class HttpHeaderField  : IParseable
    {
        protected string name;
        protected string value;

       virtual public byte[] GetBytes(Encoding encoder)
        {
            throw new NotImplementedException();
        }
       virtual public string GetString()
        {
            throw new NotImplementedException();
        }
       virtual public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }
       virtual public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }
    }
}

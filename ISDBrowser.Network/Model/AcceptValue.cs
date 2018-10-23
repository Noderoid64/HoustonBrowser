using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model
{
    public class AcceptValue : IParseable
    {
        public AcceptParam param { get; set; }
        public IParseable content { get; set; }

        public AcceptValue(IParseable content)
        {
            this.content = content;
        }
        public byte[] GetBytes(Encoding encoder)
        {
            return param.GetBytes(encoder).Concat(content.GetBytes(encoder)).ToArray();
        }

        public string GetString()
        {            
            return content.GetString() + param?.GetString();            
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

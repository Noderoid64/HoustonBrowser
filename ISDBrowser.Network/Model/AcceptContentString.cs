using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpModule.Model
{
    public class AcceptContentString : IParseable
    {
        private string content;

        public AcceptContentString(string content)
        {
            this.content = content;
        }
        public byte[] GetBytes(Encoding encoder)
        {
            return encoder.GetBytes(content);
        }

        public string GetString()
        {
            return content;
        }

        public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }

        public void SetFromString(string value)
        {
            content = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.Http.Model
{
    public class MediaRange : IParseable
    {
        public string type { get; set; }
        public string subType { get; set; }

        public byte[] GetBytes(Encoding encoder)
        {
            return encoder.GetBytes(GetString());
        }

        public string GetContent()
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            return type ?? "*" + "/" + subType ?? "*";
        }

        public void SetContent(string content)
        {
            throw new NotImplementedException();
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

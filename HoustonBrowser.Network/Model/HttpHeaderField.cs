using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpHeaderField : IParseable
    {
        public string name { get; protected set; }
        public string value { get; protected set; }

        
        #region IParseble
        virtual public byte[] GetBytes(Encoding encoder)
        {
            throw new NotImplementedException();
        }
        virtual public string GetString()
        {
            return name.ToString() + ": " + value.ToString();
        }
        virtual public void SetFromBytes(byte[] value, Encoding encoder)
        {
            throw new NotImplementedException();
        }
        virtual public void SetFromString(string value)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpHeaderField : IParseable
    {
        public string name { get; set; }
        public string value { get; set; }
        public HttpHeaderField()
        {

        }
        public HttpHeaderField(string value)
        {
            this.name = value.Substring(0, value.IndexOf(':'));
            this.value = value.Substring(value.IndexOf(':')+1);
            if(this.value.StartsWith(" "))
            this.value = this.value.Remove(0,1);

        }
        #region IParseble

        virtual public void FromString(string value)
        {
            throw new NotImplementedException();
        }
        virtual public string GetString()
        {
            return name.ToString() + ": " + value.ToString();
        }
        #endregion
    }
}

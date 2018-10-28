using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpBody : IParseable
    {
        #region IParseble
        public void FromString(string value)
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HttpBody : IParseable
    {
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> [Fix] httpPageLoader
        string content;
        public HttpBody(string content)
        {
            this.content = content;
        }
<<<<<<< HEAD
=======
>>>>>>> [Update] realize IParseble
=======
>>>>>>> [Fix] httpPageLoader
        #region IParseble
        public void FromString(string value)
        {
            content = value;
        }

        public string GetString()
        {
            return content;
        }
        #endregion
    }
}

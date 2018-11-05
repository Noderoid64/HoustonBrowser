using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Parsing
{
    public class Token<T>
    {
        T type;
        string value;
        public Token(T type,string value)
        {
            this.type = type;
            this.value = value;
        }
    }
}

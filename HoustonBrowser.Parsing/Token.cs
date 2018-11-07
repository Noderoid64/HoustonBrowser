using System;
using System.Collections.Generic;
using System.Text;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public class Token
    {
        int type;
        string value;

        public int Type { get => type; set => type = value; }
        public string Value { get => value; set => this.value = value; }

        public Token(int type,string value)
        {
            this.Type = type;
            this.Value = value;
        }
        public Token()
        {
            this.Type = (int)TokenType.Null;
            this.Value = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Parsing.Enums
{
    public enum TokenType
    {
        Text,
        NameOfTag,
        NameOfTagClosing,
        AttributeName,
        AttributeValue,
        EOF
    }
}
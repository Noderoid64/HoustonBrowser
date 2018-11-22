using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Parsing.Enums
{
    internal enum TokenType
    {
        Text,
        NameOfTag,
        NameOfTagClosing,
        AttributeName,
        AttributeValue,
        EOF
    }
}
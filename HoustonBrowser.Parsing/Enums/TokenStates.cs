using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Parsing.Enums
{
    public enum TokenStates
    {
        Data,
        RCDATA,
        RAWTEXT,
        ScriptData,
        PLAINTEXT,
        TagOpen,
        EndTagOpen,
        TagName,
        DoctypeName,
        Text,
        Attributes,
        AttributeName,
        AttributeValue
    }
}

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
        RCDATALessThanSign,
        RCDATAEndTagOpen,
        RCDATAEndTagName,
        RAWTEXTLessThanSign,
        RAWTEXTEndTagOpen,
        RAWTEXTEndTagName,

        //15+
        DoctypeName,
            Text
    }
}

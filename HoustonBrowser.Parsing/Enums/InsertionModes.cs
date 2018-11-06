using System;

namespace HoustonBrowser.Parsing.Enums
{
    enum InsertionModes
    {
        Initial,
        BeforeHtml,
        BeforeHead,
        InHead,
        InHeadNoScript,
        AfterHead,
        InBody,
        Text,
        InTable,
        InTableText,
        InCaption,
        InColumnGroup,
        InTableBody,
        InRow,
        InCell,
        InSelect,
        InSelectInTable,
        InTemplate,
        AfterBody,
        InFrameSet,
        AfterFrameset,
        AfterAfterBody,
        AfterAfterFrameset
    }
}
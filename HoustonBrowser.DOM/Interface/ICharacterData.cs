using System;

namespace HoustonBrowser.DOM.Interface
{
    public interface ICharacterData : INode
    {
        string SubstringData(long offset, long count);
        void AppendData(string arg);
        void InsertData(long offset, string arg);
        void DeleteData(long offset, long count);
        void ReplaceData(long offset, long count, string arg);
    }
}
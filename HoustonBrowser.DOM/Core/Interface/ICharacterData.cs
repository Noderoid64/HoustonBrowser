using System;

namespace HoustonBrowser.DOM.Interface
{
    public interface ICharacterData : INode
    {
        string SubstringData(int offset, int count);
        void AppendData(string arg);
        void InsertData(int offset, string arg);
        void DeleteData(int offset, int count);
        void ReplaceData(int offset, int count, string arg);
    }
}
using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM.Interface
{
    public class CharacterData : Node, ICharacterData
    {
        string data;
        long length;

        public long Length { get => length; }
        public CharacterData(TypeOfNode nodeType) :
            base(TypeOfNode.DOCUMENT_NODE, "#text", null)
        { }

        public string SubstringData(long offset, long count)
        {
            return null;
        }

        public void AppendData(string arg)
        {

        }

        public void InsertData(long offset, string arg)
        {

        }

        public void DeleteData(long offset, long count)
        {

        }

        public void ReplaceData(long offset, long count, string arg)
        {

        }
    }
}
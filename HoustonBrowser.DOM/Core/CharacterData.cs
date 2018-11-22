using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class CharacterData : Node, ICharacterData
    {
        private int length;

        public string Data { get => nodeValue; set => value = nodeValue; }
        public int Length { get => length; }

        public CharacterData(TypeOfNode nodeType, string nodeName, string data) :
            base(TypeOfNode.DOCUMENT_NODE, nodeName, data)
        {
            length = data.Length;
        }

        public string SubstringData(int offset, int count)
        {
            return Data.Substring(offset, Data.Length);
        }

        public void AppendData(string arg)
        {
            Data += arg;
            length = Data.Length;
        }

        public void InsertData(int offset, string arg)
        {
            Data = Data.Insert(offset, arg);
            length = Data.Length;
        }

        public void DeleteData(int offset, int count)
        {
            Data = Data.Remove(offset, count);
            length = Data.Length;
        }

        public void ReplaceData(int offset, int count, string arg)
        {
            Data = Data.Remove(offset, count);
            Data = Data.Insert(offset, arg);
            length = Data.Length;
        }
    }
}
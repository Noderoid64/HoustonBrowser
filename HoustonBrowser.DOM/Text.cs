using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Text : CharacterData, IText
    {
        public Text(string contentNode) :
            base(TypeOfNode.TEXT_NODE, "#text", contentNode)
        { }

        public Text(TypeOfNode nodeType, string nodeName, string contentNode) :
            base(nodeType, nodeName, contentNode)
        { }

        public Text SplitText(int offset)
        {
            string data = Data.Substring(offset, Data.Length);

            var newTextNode = new Text(data);
            this.ParentNode.AppendChild((Node)newTextNode);

            return newTextNode;
        }
    }
}
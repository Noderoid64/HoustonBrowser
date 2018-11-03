using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Comment : CharacterData
    {
        public Comment(string contentComment) :
            base(TypeOfNode.COMMENT_NODE, "#comment", contentComment)
        { }

    }
}
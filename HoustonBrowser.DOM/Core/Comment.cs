using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class Comment : CharacterData
    {
        public Comment(string contentComment) :
            base(TypeOfNode.COMMENT_NODE, "#comment", contentComment)
        { }

    }
}
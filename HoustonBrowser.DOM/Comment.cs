using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Comment : Node
    {
        public Comment(string contentComment) :
            base(TypeOfNode.ENTITY_NODE, "#comment", contentComment)
        { }

    }
}
using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Entity : Node
    {
        readonly string publicId;
        readonly string systemId;

        public string PublicId { get => publicId; }
        public string SystemId { get => systemId; }
        public string NotationName { get => nodeName; }
        public Entity(string notationName) :
            base(TypeOfNode.ENTITY_NODE, notationName, null)
        { }
    }
}
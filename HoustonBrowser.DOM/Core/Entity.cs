using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class Entity : Node
    {
        private string publicId;
        private string systemId;

        public string PublicId { get => publicId; }
        public string SystemId { get => systemId; }
        public string NotationName { get => nodeName; }
        public Entity(string notationName) :
            base(TypeOfNode.ENTITY_NODE, notationName, null)
        { }
    }
}
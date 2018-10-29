using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class EntityReference : Node
    {
        public EntityReference() :
            base(TypeOfNode.ENTITY_REFERENCE_NODE, null, null)
        { }
    }
}
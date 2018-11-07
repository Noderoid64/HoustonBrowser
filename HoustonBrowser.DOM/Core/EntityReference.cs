using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class EntityReference : Node
    {
        public EntityReference(string nameEntityRef) :
            base(TypeOfNode.ENTITY_REFERENCE_NODE, nameEntityRef, null)
        { }
    }
}
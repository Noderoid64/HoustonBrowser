using HoustonBrowser.DOM;
namespace HoustonBrowser.DOM
{
    enum AttrChangeType
    {
        Modification = 1,
        Addition,
        Removal
    }
    
    public class MutationEvent : DomEvent
    {
        public string PrevValue { get; }
        public string NewValue { get; }
        public string AttrName { get; }
        public int AttrChange { get; }

        public MutationEvent(Node target, string typeArg, bool canBubbleArg, bool cancelableArg, Node relatedNodeArg,
            string prevValueArg, string newValueArg, string attrNameArg, int attrChangeArg):base(target,typeArg,canBubbleArg,cancelableArg)
        {
            
        }
    }
}
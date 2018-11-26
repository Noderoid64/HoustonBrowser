using HoustonBrowser.DOM;
namespace HoustonBrowser.DOM
{
    enum AttrChangeType
    {
        Modification = 1,
        Addition,
        Removal
    }

    public class MutationEvent : Event
    {
        public string PrevValue { get; }
        public string NewValue { get; }
        public string AttrName { get; }
        public int AttrChange { get; }

        void InitMutationEvent(string typeArg, bool canBubbleArg, bool cancelableArg, Node relatedNodeArg,
            string prevValueArg, string newValueArg, string attrNameArg, int attrChangeArg)
        {
            
        }
    }
}
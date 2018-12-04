namespace HoustonBrowser.DOM
{
    public class UIEvent : DomEvent
    {
        protected AbstractView view;
        protected int detail;

        public AbstractView View { get => view; }
        public int Detail { get => detail; }

        public UIEvent(Node target, string typeArg, bool canBubbleArg, bool canCancelableArg, AbstractView viewArg, int detailArg):base(target,typeArg,canBubbleArg,canCancelableArg)
        {

        }
    }
}
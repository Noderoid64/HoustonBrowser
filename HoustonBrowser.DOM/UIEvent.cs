namespace HoustonBrowser.JS
{
    class UIEvent : Event
    {
        public AbstractView View { get; }
        public int Detail { get; }
        public void InitUIEvent(string typeArg, bool canBubbleArg, bool canCancelableArg, AbstractView viewArg, int detailArg)
        {

        }
    }
}
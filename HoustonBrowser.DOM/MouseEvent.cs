namespace HoustonBrowser.DOM
{
    class MouseEvent : UIEvent
    {
        public int ScreenX { get; }
        public int ScreenY { get; }
        public int ClientX { get; }
        public int ClientY { get; }
        public bool CtrlKey { get; }
        public bool ShiftKey { get; }
        public bool AltKey { get; }
        public bool MetaKey { get; }
        public int Button { get; }
        public EventTarget RelatedTarget { get; }
        public void InitMouseEvent(string typeArg, bool canBubbleArg, bool cancelableArg, AbstractView viewArg,
            int detailArg, int screenXArg, int screenYArg, int clientXArg, int clientYArg, bool ctrlKeyArg, bool altKeyArg,
            bool shiftKeyArg, bool metaKeyArg, int buttonArg, EventTarget relatedTargetArg)
        {

        }
    }
}
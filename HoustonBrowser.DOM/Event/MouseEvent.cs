namespace HoustonBrowser.DOM
{
    public class MouseEvent : UIEvent
    {
        private int screenX;
        protected int screenY  ;
        protected int clientX  ;
        protected int clientY  ;
        protected bool ctrlKey ;
        protected bool shiftKey;
        protected bool altKey  ;
        protected bool metaKey ;
        protected int button;
        protected IEventTarget relatedTarget;

        public MouseEvent(Node target, string typeArg, bool canBubbleArg, bool cancelableArg, AbstractView viewArg,
            int detailArg, int screenXArg, int screenYArg, int clientXArg, int clientYArg, bool ctrlKeyArg, bool altKeyArg,
            bool shiftKeyArg, bool metaKeyArg, int buttonArg, IEventTarget relatedTargetArg):base(target,typeArg, canBubbleArg, cancelableArg, viewArg, detailArg)
        {                       
            this.screenX = screenXArg;
            this.screenY = screenYArg;
        }
    }
}
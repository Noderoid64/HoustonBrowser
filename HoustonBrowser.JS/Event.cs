using System;

namespace HoustonBrowser.JS
{
    class Event
    {
        public readonly string type;
        public readonly EventTarget target;
        public readonly EventTarget turrentTarget;
        public readonly EventPhase eventPhase;
        public readonly bool bubbles;
        public readonly bool cancelable;
        public readonly DateTime timeStamp;

        public void StopPtopagation()
        {

        }
        public void PreventDefault()
        {

        }
        public void InitEvent(string eventTypeArg, bool canBubbleArg, bool cancelableArg)
        {

        }
    }
}
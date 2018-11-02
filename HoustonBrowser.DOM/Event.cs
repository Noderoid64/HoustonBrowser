using System;

namespace HoustonBrowser.DOM
{
     enum PhaseType
    {
        CapturingPhase = 1,
        AtTarget,
        BubblingPhase
    }
    class Event
    {   
        public string Type { get; }
        public EventTarget Target { get; }
        public EventTarget CurrentTarget { get; }
        public PhaseType EventPhase { get; }
        public bool Bubbles { get; }
        public bool Cancelable { get; }
        public DateTime TimeStamp { get; }

        public void StopPropagation()
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
using System;

namespace HoustonBrowser.DOM
{
    public enum PhaseType
    {
        CapturingPhase = 1,
        AtTarget,
        BubblingPhase
    }

    public class Event
    {   
        public string Type { get; }
        public EventTarget Target { get; }
        public Node CurrentNode { get; }
        public PhaseType EventPhase { get; }
        public bool Bubbles { get; }
        public bool Cancelable { get; }

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
using System;

namespace HoustonBrowser.DOM
{
    public enum PhaseType
    {
        CapturingPhase = 1,
        AtTarget,
        BubblingPhase
    }

    public class DomEvent
    {
        protected string type;
        protected IEventTarget target;
        protected Node currentNode;
        protected PhaseType eventPhase;
        protected bool bubbles;
        protected bool cancelable;

        public string Type { get { return type; } }
        public IEventTarget Target { get { return target; } }
        public Node CurrentNode { get { return currentNode; } }
        public PhaseType EventPhase { get { return eventPhase; } }
        public bool Bubbles { get { return bubbles; } }
        public bool Cancelable { get { return cancelable; } }

        public void StopPropagation()
        {

        }

        public void PreventDefault()
        {

        }

        public DomEvent(Node target, string eventTypeArg, bool canBubbleArg, bool cancelableArg)

        {
            this.target = target;
            this.currentNode = target;
            this.bubbles = canBubbleArg;
            this.type = eventTypeArg;
            this.cancelable = cancelableArg;
        }
    }
}
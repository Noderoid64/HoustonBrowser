using System;

namespace HoustonBrowser.DOM
{
    public class EventListener
    {
        public event EventHandler<DomEvent> HandleDomEvent;

        public void Trigger(object sender,DomEvent @event)
        {
            HandleDomEvent(sender, @event);
        }
    }
}
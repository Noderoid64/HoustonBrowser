using System;

namespace HoustonBrowser.DOM
{
    public interface IEventTarget
    {
        void AddEventListener(string type, EventListener listener, bool useCapture);
        void RemoveEventListener(string type, EventListener listener, bool useCapture);
        bool DispatchEvent(DomEvent @event);
    }
}
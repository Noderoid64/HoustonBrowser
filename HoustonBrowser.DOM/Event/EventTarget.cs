using System;

namespace HoustonBrowser.DOM
{
    public interface EventTarget
    {
        void AddEventListener(string type, EventListener listener, bool useCapture);

        void RemoveEventListener(string type, EventListener listener, bool useCapture);

        bool DispatchEvent(Event evt);
    }
}
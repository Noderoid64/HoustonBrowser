using System;

namespace HoustonBrowser.DOM
{
    class EventTarget
    {
        
        public void AddEventListener(string type, EventListener listener, bool useCapture)
        {
            return;
        }
        
        public void RemoveEventListener(string type, EventListener listener, bool useCapture)
        {
            return;
        }
        
        public bool DispatchEvent(Event evt)
        {
            return true;
        }
    }
}
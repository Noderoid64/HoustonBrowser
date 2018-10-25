using System;

namespace ISDBrowser.JS
{
    class EventTarget
    {
        public void AddEventListener(string type, EventListener listener, bool useCapture)
        {

        }
        public void RemoveEventListener(string type, EventListener listener, bool useCapture)
        {

        }
        public bool DispatchEvent(Event evt)
        {
            //This method can raise a EventException object.
            return true;
        }
    }
}
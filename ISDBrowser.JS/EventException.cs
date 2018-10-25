using System;

namespace ISDBrowser.JS
{
    enum EventExceptionType
    {
        UnspecifiedEventTypeError = 0
    }
    class EventException
    {
        public int Code { get; set; }
    }
}
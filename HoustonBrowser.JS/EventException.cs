using System;

namespace HoustonBrowser.JS
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
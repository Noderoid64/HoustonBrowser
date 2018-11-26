using System;

namespace HoustonBrowser.DOM
{
    enum EventExceptionType
    {
        UnspecifiedEventTypeError
    }

    public class EventException
    {
        public int Code { get; set; }
    }
}
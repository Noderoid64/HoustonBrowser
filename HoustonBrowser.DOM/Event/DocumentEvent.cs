using System;

namespace HoustonBrowser.DOM
{

    interface IDocumentEvent
    {
        DomEvent CreateEvent(string eventType); 
    }
}
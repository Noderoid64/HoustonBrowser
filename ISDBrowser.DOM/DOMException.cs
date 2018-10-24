using System;

namespace ISDBrowser.DOM
{
    public class DOMException : Exception
    {
        public DOMException(): base() {}
        public DOMException(string massage): base(massage) { }
        public DOMException(string massage, Exception exception): 
                                        base(massage, exception) { }
    }
}
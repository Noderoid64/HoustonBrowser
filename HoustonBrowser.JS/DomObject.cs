using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;


namespace HoustonBrowser.JS
{
    class DomObject : HostObject
    {
        public DomObject(HostObject proto, string @class, Func<HostObject, List<Primitive>, Primitive> callMethod = null, Func<HostObject, List<Primitive>, Primitive> constructMethod = null) : base(proto, @class, callMethod, constructMethod)
        {
        }

        public Node Node { get; set; }
    }
}

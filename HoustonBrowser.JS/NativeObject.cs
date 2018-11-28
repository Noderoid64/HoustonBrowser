using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.JS
{
    class NativeObject : HostObject
    {
        UnaryExpression callBody;

        public NativeObject(HostObject proto, string @class, UnaryExpression callBody=null) : base(proto, @class)
        {
            this.callBody = callBody;
        }

        public UnaryExpression CallBody { get => callBody; set => callBody = value; }

        public override Primitive Call(HostObject @this, Primitive arguments) // not by spec. see page 81
        {
            HostObject execContext = new HostObject(null, "Object");
            ESInterpreter interpreter = new ESInterpreter(@this);
            interpreter.Process(callBody);
            return new Primitive(ESType.Undefined, null);
        }

        public override HostObject Construct(HostObject @this, Primitive arguments)
        {
            NativeObject newObj = new NativeObject(prototype, "Object");
            Call(newObj, arguments);
            return newObj;
        }
    }
}

using System;
using System.Collections.Generic;

namespace HoustonBrowser.JS
{
    class HostObject : Primitive
    {
        private HostObject prototype;
        private string @class;
        private HostObject scope;
        private Func<HostObject, List<Primitive>, Primitive> callMethod;
        private Dictionary<string, Property> properties = new Dictionary<string, Property>();

        public Func<HostObject, List<Primitive>, Primitive> CallMethod { get => callMethod; set => callMethod = value; }
        public HostObject Scope { get => scope; set => scope = value; }
        public string Class { get => @class; set => @class = value; }
        internal Dictionary<string, Property> Properties { get => properties; set => properties = value; }
        internal HostObject Prototype { get => prototype; set => prototype = value; }

        public HostObject(HostObject proto, string @class, Func<HostObject, List<Primitive>, Primitive> callMethod = null)
        {
            this.prototype = proto;
            this.@class = @class;
            this.callMethod = callMethod;
        }

        public Primitive Get(string p)
        {
            HostObject hostObject = this;
            while (hostObject != null)
            {
                if (properties.ContainsKey(p)) return properties[p].Value;
                hostObject = hostObject.prototype;
            }
            Primitive prop = new Primitive(ESType.Undefined, null);
            properties.Add(p, new Property(0, prop));
            return prop;
        }

        public void Put(string p, Primitive v, Attributes attributes = 0)
        {
            if (CanPut(p))
            {
                if (properties.ContainsKey(p)) properties[p].Value = v;
                else properties.Add(p, new Property(attributes, v));
            }
        }

        public bool CanPut(string p)
        {
            HostObject hostObject = this;
            while (true)
            {
                if (properties.ContainsKey(p))
                {
                    Property tuple = hostObject.properties[p];
                    if ((tuple.Attrs & Attributes.ReadOnly) == Attributes.ReadOnly) return false;
                    return true;
                }
                if (hostObject.prototype == null) return true;
                hostObject = hostObject.prototype;
            }
        }

        public bool HasProperty(string p)
        {
            HostObject hostObject = this;
            while (hostObject != null)
            {
                if (hostObject.properties.ContainsKey(p)) return true;
                hostObject = hostObject.prototype;
            }
            return false;
        }

        public bool Delete(string p)
        {
            if (!properties.ContainsKey(p)) return true;
            Property tuple = properties[p];
            if ((tuple.Attrs & Attributes.DontDelete) == Attributes.DontDelete) return false;
            return properties.Remove(p); // = return true
        }

        public void DefaultValue(ESType hint = ESType.Number)// TODO
        {
            //Primitive obj = Get("toString");
            //if (obj.Type == ESType.Object)
            //{
            //    Primitive res = (obj as VarObject).Call(null);
            //    if (res.Type != ESType.Object) return res;
            //}
        }

        public virtual Primitive Call(HostObject @this, Primitive arguments)// not by spec
        {
            return callMethod.Invoke(@this, arguments.Value as List<Primitive>);
        }

        //public virtual HostObject Construct(Primitive arguments)
        //{
        //    return callMethod.Invoke(arguments.Value as List<Primitive>) as HostObject;
        //}
    }
}

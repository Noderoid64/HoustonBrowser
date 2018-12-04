using System;

namespace HoustonBrowser.JS
{
    class Property
    {
        private Attributes attrs;
        private Primitive value;
        protected Func<Primitive, Primitive> setter;

        public Property()
        {
        }

        public Property(Attributes attrs, Primitive value)
        {
            this.attrs = attrs;
            this.value = value;
        }

        public Property(Attributes attrs, Primitive value, Func<Primitive, Primitive> setter)
        {
            this.attrs = attrs;
            this.value = value;
            this.setter = setter;

        }

        internal Attributes Attrs { get => attrs; set => attrs = value; }
        internal Primitive Value { get => value; set {
                if (setter != null) this.value = setter(value);
                else this.value = value;
            }
        }
    }
}

namespace HoustonBrowser.JS
{
    class Property
    {
        private Attributes attrs;
        private Primitive value;

        public Property()
        {
        }

        public Property(Attributes attrs, Primitive value)
        {
            this.attrs = attrs;
            this.value = value;
        }

        internal Attributes Attrs { get => attrs; set => attrs = value; }
        internal Primitive Value { get => value; set => this.value = value; }
    }
}

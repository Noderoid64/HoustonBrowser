namespace HoustonBrowser.JS
{
    class Primitive
    {
        private ESType type;
        private object value;

        public Primitive()
        {
        }

        public Primitive(ESType type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public object Value { get => value; set => this.value = value; }
        public ESType Type { get => type; set => type = value; }
    }
}

using System;

namespace HoustonBrowser.DOM
{
    public class Attr : Node
    {
        string name;
        bool specified;
        public string Name { get => name; }
        public bool Specified { get => specified; }
        public string Value { get; set; }

        public Attr(string name)
        {
            this.name = name;
        }
    }
}
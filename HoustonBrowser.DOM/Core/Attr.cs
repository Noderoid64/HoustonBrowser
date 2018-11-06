using System;

namespace HoustonBrowser.DOM
{
    public class Attr : Node
    {
        private bool specified;

        public string Name { get => nodeName; }
        public bool Specified { get => specified; }
        public string Value { get => nodeValue; set => value = nodeValue; }

        public Attr(string name) :
                    base(TypeOfNode.ATTRIBUTE_NODE, name, null)
        { }

        public Attr(string name, string value) :
                    base(TypeOfNode.ATTRIBUTE_NODE, name, value)
        { }
    }
}
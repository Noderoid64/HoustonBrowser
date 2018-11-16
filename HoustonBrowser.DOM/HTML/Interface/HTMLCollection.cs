using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class HTMLCollection : IHTMLCollection
    {
        int length;
        public int Length { get; }
        public Node Item()
        {
            return null;
        }
        public Node NamedItem()
        {
            return null;
        }
    }
}
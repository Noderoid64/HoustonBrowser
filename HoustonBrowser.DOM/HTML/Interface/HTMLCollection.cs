using HoustonBrowser.DOM.Core;
using HoustonBrowser.DOM.HTML.Interface;

namespace HoustonBrowser.DOM.HTML
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
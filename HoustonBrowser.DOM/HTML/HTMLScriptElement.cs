using System;
namespace HoustonBrowser.DOM.HTML
{
    public class HTMLScriptElement : HTMLElement
    {
        public string Text { get; set; }
        public string HTMLFor { get; set; }
        public string Event { get; set; }
        public string Charset { get; set; }
        public bool Defer { get; set; }
        public string Src { get; set; }
        public string Type { get; set; }

        public HTMLScriptElement() : base("script") { }
    }
}
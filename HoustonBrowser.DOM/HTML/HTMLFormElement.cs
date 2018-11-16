using System;
namespace HoustonBrowser.DOM
{
    public class HTMLFormElement : HTMLElement, IHTMLFormElement
    {

        public string Name { get; set; }
        public string AcceptCharset { get; set; }
        public string Action { get; set; }
        public string Enctype { get; set; }
        public string Method { get; set; }
        public string Target { get; set; }

        public HTMLFormElement() : base("form") { }

        public void Submit() { }
        public void Reset() { }
    }
}
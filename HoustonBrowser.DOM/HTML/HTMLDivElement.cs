using System;
namespace HoustonBrowser.DOM.HTML
{
    public class HTMLDivElement: HTMLElement
    {
        public string Align { get; set; }

        public HTMLDivElement(): base("div") {}
    }
}
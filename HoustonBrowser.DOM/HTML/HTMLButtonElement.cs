using System;
namespace HoustonBrowser.DOM
{
    public class HTMLButtonElement : HTMLElement
    {

        readonly HTMLFormElement form;
        string accessKey;
        bool disabled;
        string name;
        int tabIndex;
        readonly string type;
        string value;
        public HTMLButtonElement() : base("button") { }

    }
}
using System;
using HoustonBrowser.HttpModule;
using HoustonBrowser;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HoustonBrowser.Controls;
using HoustonBrowser.JS;
using HoustonBrowser.DOM;
using HoustonBrowser.Parsing;

namespace HoustonBrowser.Core
{
    public class Core
    {
        IHttpClient httpClient;
        IParser parser;
        IBrowserControl control;
        IJS js;
        IUI ui;
        MockDocument dom;

        public IJS Js { get => js;}

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui)
        {
            this.ui = ui;
            this.httpClient = new MockHttpClient();
            this.parser = new mockParser();
            this.control=new BrowserControl();
            this.js=new MockJS();
            this.dom = new MockDocument();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;

            //button.Click+=Button_onMouseClick;

            parser = new Parser();
            parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nfunction myFunction() {\r\n    var x = document.getElementById(\"myDIV\");\r\n    if (x) {\r\n      x.style.display = \"none\";\r\n    }\r\n}\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>");

        }

        private void Ui_onPageLoad(object sender, object e)
        {

        }

        private void Ui_onMouseClick(object sender, PointerPressedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" +parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            //onRender(this, new RenderEventArgs(s));
        }

        private void Ui_onKeyDown(object sender, KeyEventArgs e)
        {

            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            //onRender(this, new RenderEventArgs(s));
        }

        private void Button_onMouseClick(object sender, RoutedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            //onRender(this, new RenderEventArgs(s));
        }
    }
}

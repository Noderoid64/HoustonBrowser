using System;
using System.Collections.Generic;
using HoustonBrowser.HttpModule;
using HoustonBrowser;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HoustonBrowser.Controls;
using HoustonBrowser.JS;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.HTML;
using HoustonBrowser.Parsing;
using HoustonBrowser.Render;


namespace HoustonBrowser.Core
{
    public class Core
    {
        INetworkClient httpClient;
        IParser parser;
        IBrowserControl control;
        IJS js;
        IUI ui;
        MockDocument dom;

        RenderTree renderTree;

        public IJS Js { get => js;}

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui)
        {
            this.ui = ui;

            this.httpClient = new MockNetworkClient();
            this.parser = new Parser();

            this.control=new BrowserControl();
            this.js=new MockJS();
            this.dom = new MockDocument();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;
            parser.onNonHtmlEvent += Parser_onNonHtmlEvent;

            // button.Click+=Button_onMouseClick;

       
            //var doc = parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nfunction myFunction() {\r\n    var x = document.getElementById(\"myDIV\");\r\n    if (x) {\r\n      x.style.display = \"none\";\r\n    }\r\n}\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>");
            //var renderTree = new RenderTree(doc);
            //var tmp = renderTree.GetPage();
            
        }

        private void Parser_onNonHtmlEvent(object sender, string e)
        {
            js.Process(e);
        }

        private void Ui_onPageLoad(object sender, PageLoadEventArgs e)
        {
            //var doc = parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nalert(TEST)\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>");
            //RenderTree renderTree = new RenderTree(parser.Parse("<html>\r\n<head>\r\nHoustonBrowser\r\n</head>\r\n<body>\r\n<script>\r\nfunction myFunction() {\r\n    var x = document.getElementById(\"myDIV\");\r\n    if (x) {\r\n      x.style.display = \"none\";\r\n    }\r\n}\r\n</script>\r\n\r\n<button onclick=\"myFunction()\">Click Me</button>\r\n\r\n<div id=\"myDIV\">\r\n  This is my DIV element.\r\n</div>\r\n</body>\r\n</html>"));
            RenderTree renderTree = new RenderTree(parser.Parse(httpClient.Get(e.UrlString)));
            RenderEventArgs renderEventArgs = new RenderEventArgs(renderTree.GetPage());
            onRender(this,renderEventArgs);
        }

        private void Ui_onMouseClick(object sender, PointerPressedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" +parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
            onRender(this, new RenderEventArgs(null));

        }

        private void Ui_onKeyDown(object sender, KeyEventArgs e)
        {

            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
            onRender(this, new RenderEventArgs(null));
        }

        private void Button_onMouseClick(object sender, RoutedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
            onRender(this, new RenderEventArgs(null));

        }
    }
}

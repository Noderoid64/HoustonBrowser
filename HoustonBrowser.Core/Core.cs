using System;
using System.Collections.Generic;
using HoustonBrowser.HttpModule;
using HoustonBrowser;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HoustonBrowser.Controls;
using HoustonBrowser.JS;
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
        IRenderTree renderTree;

        public IJS Js { get => js; }

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui)
        {
            this.ui = ui;

            this.httpClient = new NetworkClient();
            this.parser = new Parser();

            this.control = new BrowserControl();
            this.js = new MockJS();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;
            parser.onNonHtmlEvent += Parser_onNonHtmlEvent;
        }

        private void Parser_onNonHtmlEvent(object sender, string e)
        {
            js.Process(e);
        }

        private void Ui_onPageLoad(object sender, PageLoadEventArgs e)
        {
            renderTree = new RenderTree(parser.Parse(httpClient.Get(e.UrlString)));
            var tmp = renderTree.GetPage();
            RenderEventArgs renderEventArgs = new RenderEventArgs(renderTree.GetPage());
           
            onRender(this, renderEventArgs);
        }

        private void Ui_onMouseClick(object sender, PointerPressedEventArgs e)
        {
            string s = httpClient.GetStatus() + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render();
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

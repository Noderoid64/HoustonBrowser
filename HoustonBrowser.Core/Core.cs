using System;
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
            this.httpClient = new HttpClient();
            this.control=new BrowserControl();
            this.js=new MockJS();
            this.dom = new MockDocument();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;

            // button.Click+=Button_onMouseClick;

            parser = new Parser();
            //var renderTree = new RenderTree(doc);
            //renderTree.GetPage();
        }

        public void PageLoading(string url)
        {
            parser.Parse(httpClient.GetHtml(url));
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

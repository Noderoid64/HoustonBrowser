using System;
using HoustonBrowser.HttpModule;
using HoustonBrowser;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using HoustonBrowser.Controls;
using HoustonBrowser.JS;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;
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
        IDocument dom;

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

        }

        private void Ui_onPageLoad(object sender, object e)
        {

        }

        private void Ui_onMouseClick(object sender, PointerPressedEventArgs e)
        {
            string s = httpClient.GetHtml("") + "\n" +parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            onRender(this, new RenderEventArgs(s));
        }

        private void Ui_onKeyDown(object sender, KeyEventArgs e)
        {
            string s = httpClient.GetHtml("") + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            onRender(this, new RenderEventArgs(s));
        }

        private void Button_onMouseClick(object sender, RoutedEventArgs e)
        {
            string s = httpClient.GetHtml("") + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            onRender(this, new RenderEventArgs(s));
        }
    }
}

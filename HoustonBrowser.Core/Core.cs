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

namespace HoustonBrowser.Core
{
    public class Core
    {
        IHttpClient httpClient;
        IParser parser;
        IBrowserControl control;
        IJS js;
        IUI ui;
        Avalonia.Controls.Button button;
        IDocument dom;

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui, Avalonia.Controls.Button button)
        {
            this.ui = ui;
            this.httpClient = new MockHttpClient();
            this.parser = new mogParser();
            this.control=new BrowserControl();
            this.js=new MockJS();
            this.button=button;
            this.dom = new MockDocument();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;

            button.Click+=Button_onMouseClick;

        }

        private void Ui_onPageLoad(object sender, object e)
        {

        }

        private void Ui_onMouseClick(object sender, PointerPressedEventArgs e)
        {
            string s = httpClient.GET("") + "\n" +parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            onRender(this, new RenderEventArgs(s));
        }

        private void Ui_onKeyDown(object sender, KeyEventArgs e)
        {
            string s = httpClient.GET("") + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            onRender(this, new RenderEventArgs(s));
        }

        private void Button_onMouseClick(object sender, RoutedEventArgs e)
        {
            string s = httpClient.GET("") + "\n" + parser.Parse() + "\n" + js.Process("") + "\n" + control.Render()+"\n"+dom.DomWork();
            onRender(this, new RenderEventArgs(s));
        }
    }
}

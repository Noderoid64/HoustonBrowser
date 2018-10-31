using System;
using HoustonBrowser.HttpModule;
using HoustonBrowser;


namespace HoustonBrowser.Core
{
    public class Core
    {
        IHttpClient httpClient;
        IParser parser;
        IUI ui;

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui)
        {
            this.ui = ui;
            this.httpClient = new MockHttpClient();
            this.parser = new mogParser();

            ui.onKeyDown += Ui_onKeyDown;
            ui.onMouseClick += Ui_onMouseClick;
            ui.onPageLoad += Ui_onPageLoad;
        }

        private void Ui_onPageLoad(object sender, object e)
        {

        }

        private void Ui_onMouseClick(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            string s = httpClient.GET("") + parser.Parse();
            onRender(this, new RenderEventArgs(s));
        }

        private void Ui_onKeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            string s = httpClient.GET("") + parser.Parse();
            onRender(this, new RenderEventArgs(s));
        }
    }
}

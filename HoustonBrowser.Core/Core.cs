using System.IO;

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
      //  RenderPage renderTree;

        public IJS Js { get => js; }

        public event EventHandler<RenderEventArgs> onRender;

        public Core(IUI ui)
        {
            this.ui = ui;

            this.httpClient = new NetworkClient();
            this.parser = new Parser();

            this.control=new BrowserControl();
            this.js=new JSModule();
            


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
           

            if (e.UrlString != null)
            {
                RenderPage renderTree = new RenderPage(parser.Parse(httpClient.Get(e.UrlString)));
                RenderEventArgs renderEventArgs = new RenderEventArgs(renderTree.ListOfControls);
                onRender(this, renderEventArgs);
            }
            else
            {
                string str = string.Empty;
                string ln = string.Empty;
                RenderPage renderTree;

                using (StreamReader stream = new StreamReader("file.txt"))
                {
                    ln = stream.ReadLine();

                    do
                    {
                        str += ln + "\r\n";
                        ln = stream.ReadLine();
                    }
                    while (ln != null);

                    renderTree = new RenderPage(parser.Parse(str));
                    RenderEventArgs renderEventArgs = new RenderEventArgs(renderTree.ListOfControls);
                    onRender(this, renderEventArgs);
                }
            }
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

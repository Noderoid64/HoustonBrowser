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
using HoustonBrowser.DOM;

namespace HoustonBrowser.Core
{
    public class Core
    {
        INetworkClient httpClient;
        IParser parser;
        IBrowserControl control;
        IJS js;
        IUI ui;
        RenderPage renderTree;
        Queue<DomEvent> eventsQueue = new Queue<DomEvent>();

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
            HTMLDocument dom = parser.Parse(httpClient.Get(e.UrlString));
            renderTree = new RenderPage(dom);
            RegisterEvents();
            RenderEventArgs renderEventArgs = new RenderEventArgs(renderTree.ListOfControls);
            onRender(this, renderEventArgs);
        }

        private void Control_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            Node node = renderTree.DomNodes[sender as BrowserControl];
            MouseEvent mouseEvent = new MouseEvent(node, "mousedown", true, true, null, 0, 10, 10, 10, 10, false, false, false, false, 0, null);
            node.DispatchEvent(mouseEvent);
        }

        private void Listener_HandleDomEvent(object sender, DomEvent e)
        {
            eventsQueue.Enqueue(e);
            ProcessEvents();
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

        private void RegisterEvents()
        {
            foreach (var control in renderTree.ListOfControls)
            {
                if (renderTree.DomNodes[control].Attributes != null)
                {
                    foreach (KeyValuePair<string, Node> atr in renderTree.DomNodes[control].Attributes)
                    {
                        switch (atr.Key)
                        {
                            case "onmousedown":
                                EventListener listener = new EventListener();
                                listener.HandleDomEvent += Listener_HandleDomEvent;
                                renderTree.DomNodes[control].AddEventListener("mousedown", listener, false);
                                control.PointerPressed += Control_PointerPressed;
                                break;
                            default:
                                break;
                        }
                    }

                }
            }

        }

        private void ProcessEvents()
        {
            DomEvent @event;
            while (eventsQueue.Count != 0)
            {
                @event = eventsQueue.Dequeue();
                if (@event.Bubbles)
                {
                    Node node = @event.CurrentNode;
                    while (node != null)
                    {
                        Node atr;
                        if (node.Attributes!=null && (atr=node.Attributes.GetNamedItem("on" + @event.Type)) != null)
                        {
                            js.Process(atr.NodeValue);
                        }
                        node = node.ParentNode;
                    }
                }
            }
        }


    }
}

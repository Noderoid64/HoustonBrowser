using System;
using System.Collections.Generic;
using Avalonia.Media;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Core;


namespace HoustonBrowser.Render
{
    public class RenderTree
    {
        private Document document;

        public RenderTree(Document document)
        {
            this.document = document;
        }

        public List<BrowserControl> GetPage()
        {
            int left = 0;
            int top = 0;
            return GetPage(document, left, top);
        }

        public List<BrowserControl> GetPage(Node node, int left, int top)
        {
            var listControls = new List<BrowserControl>();

            switch (node.NodeName)
            {
                case ("button"):
                    var button = new Button();
                    button.Text = "button1";
                    button.IsDefault = true;
                    button.Top = 10;
                    button.Left = 30;
                    listControls.Add(button);

                    break;
                case ("div"):
                    //var div = new Rectangle();
                    //div.IsDefault = true;
                    //div.Top = left;
                    //div.Left = top;
                    //div.Text = node.NodeValue;
                    //listControls.Add(div);
                    //left += 50;
                    break;
                case ("#text"):
                    var label = new Label();
                    label.IsDefault = true;
                    label.Top = left;
                    label.Left = top;
                    label.Text = node.NodeValue;
                    listControls.Add(label);
                    break;
                case ("p"):
                    listControls.Add(new Label());
                    break;
            }

            if (node.ChildNodes.Count != 0)
            {
                foreach (Node tmpNode in node.ChildNodes)
                {
                    var list = GetPage(tmpNode, left, top);
                    listControls.AddRange(list);
                }
            }

            return listControls;
        }
    }
}

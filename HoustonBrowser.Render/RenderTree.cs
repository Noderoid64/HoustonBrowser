using System;
using System.Collections.Generic;
using Avalonia.Media;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM;


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
            double left = 0;
            double top = 0;
            return GetPage(document, left, top);
        }

        public List<BrowserControl> GetPage(Node node, double left, double top)
        {
            var listControls = new List<BrowserControl>();

            switch (node.NodeName)
            {
                case ("button"):
                    var button = new Button();
                    button.Text = node.NodeValue;
                    left += button.Width;
                    listControls.Add(button);

                    break;
                case ("div"):
                    top += 30;
                    var div = new Rectangle();
                    listControls.Add(div);
                    top += div.Height / 3.5;
                    left = 0;
                    break;
                case ("#text"):
                    var label = new Label();
                    label.Text = node.NodeValue;
                    left += label.Width;
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

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
            return GetPage(document);
        }

        public List<BrowserControl> GetPage(Node node)
        {
            var listControls = new List<BrowserControl>();

            switch (node.NodeName)
            {
                case ("button"):
                    var button = new Button();
                    button.Text = "button1";

                    listControls.Add(new Button());
                    break;
                case ("div"):
                    var div = new Rectangle();
                    div.BorderBrush = new SolidColorBrush(new Color(255, 255, 0, 0));
                    div.Top = 10;
                    div.Left = 30;
                    div.Text = node.NodeValue;
                    listControls.Add(div);
                    break;
                case ("p"):
                    listControls.Add(new Label());
                    break;
            }

            if (node.ChildNodes.Count != 0)
            {
                foreach (Node tmpNode in node.ChildNodes)
                {
                    var list = GetPage(tmpNode);
                    listControls.AddRange(list);
                }
            }
<<<<<<< HEAD
<<<<<<< HEAD

            return listControls;
=======
<<<<<<< HEAD
=======

            return listControls;
>>>>>>> process
>>>>>>> process
=======

            return listControls;
>>>>>>> fix
        }
    }
}

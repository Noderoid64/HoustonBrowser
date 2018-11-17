using System;
using System.Collections.Generic;
using Avalonia.Media;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;


namespace HoustonBrowser.Render
{
    public enum TypeOfNode
    {
        ELEMENT_NODE = 1,
        ATTRIBUTE_NODE,
        TEXT_NODE,
        CDATA_SECTION_NODE,
        ENTITY_REFERENCE_NODE,
        ENTITY_NODE,
        PROCESSING_INSTRUCTION_NODE,
        COMMENT_NODE,
        DOCUMENT_NODE,
        DOCUMENT_TYPE_NODE,
        DOCUMENT_FRAGMENT_NODE,
        NOTATION_NODE
    }
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

<<<<<<< HEAD
=======
            switch (node.NodeName)
            {
                case ("button"):
                    var button = new Button(){Left=left, Top=top};
                    button.Text = node.NodeValue;
                    left += button.Width;
                    listControls.Add(button);

                    break;
                case ("div"):
                    top += 30;
                    var div = new Rectangle(){Left=left, Top=top};
                    listControls.Add(div);
                    top += div.Height / 3.5;
                    left = 0;
                    break;
                case ("#text"):
                    var label = new Label(){Left=left, Top=top};
                    label.Text = node.NodeValue;
                    left += label.Width;
                    listControls.Add(label);
                    break;
                case ("p"):
                    listControls.Add(new Label());
                    break;
            }

>>>>>>> a0d2a13fddf494eb895a0491b39183d56f91eb3d
            if (node.ChildNodes.Count != 0)
            {
                foreach (Node tmpNode in node.ChildNodes)
                {
                    var list = GetPage(tmpNode, left, top);
                    listControls.AddRange(list);
                }
            }

            switch (node.NodeType)
            {
                case ((int)TypeOfNode.ATTRIBUTE_NODE):
                break;
                case ((int)TypeOfNode.TEXT_NODE):
                break;
                case ((int)TypeOfNode.ELEMENT_NODE):
                break;

            }

            return listControls;
        }
    }
}

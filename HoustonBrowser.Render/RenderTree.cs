using System;
using System.Collections.Generic;
using Avalonia.Media;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class RenderTree : IRenderTree
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

        private HTMLDocument document;

        public RenderTree(HTMLDocument document)
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

        private BrowserControl GetControlElement(Node node)
        {
            return new BrowserControl();
        }
    }
}

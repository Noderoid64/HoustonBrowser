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

        public static double Width { get; set; } = 920;
        public double Height { get; set; } = 100;

        private delegate BrowserControl GetControl(
            out double controlsWidth,
            out double controlsHeight,
            Node node,
            double left,
            double top
            );

        private static Dictionary<Type, GetControl> ControlsDictionary
            = new Dictionary<Type, GetControl>
            {
                [typeof(HTMLBodyElement)] = GetBodyControl,
                // [typeof(HTMLButtonElement)] = GetButtonControl,
                // [typeof(HTMLDivElement)] = GetDivControl,
                // [typeof(HTMLFormElement)] = GetFormControl,
                // [typeof(HTMLH1Element)] = GetHeadingControl,
                // [typeof(HTMLH2Element)] = GetHeadingControl,
                // [typeof(HTMLH3Element)] = GetHeadingControl,
                // [typeof(HTMLH4Element)] = GetHeadingControl,
                // [typeof(HTMLH5Element)] = GetHeadingControl,
                // [typeof(HTMLH6Element)] = GetHeadingControl,
                [typeof(HTMLParagraphElement)] = GetParagraphControl,
            };



        public RenderTree(HTMLDocument document)
        {
            this.document = document;
        }

        public List<BrowserControl> GetPage()
        {
            double left;
            double top;
            return GetControls(out left, out top, document, 0, 0);
        }

        public static List<BrowserControl> GetControls(
            out double controlsWidth,
            out double controlsHeight,
            Node node,
            double left,
            double top
            )
        {
            controlsWidth = 0;
            controlsHeight = 0;
            double controlHeight = 0;
            double controlWidth = 0;

            GetControl GetControl;
            var newListControls = new List<BrowserControl>();
            var listControls = new List<BrowserControl>();

            if (node.ChildNodes.Count != 0)
            {
                foreach (Node tmpNode in node.ChildNodes)
                {
                    listControls.AddRange(GetControls(out controlHeight, out controlWidth, tmpNode, controlHeight, Width - left));

                    controlsHeight += controlHeight;

                    if (controlWidth > controlsHeight)
                        controlsWidth = controlWidth;
                }
            }

            switch (node.NodeType)
            {
                case ((int)TypeOfNode.ELEMENT_NODE):

                    if (ControlsDictionary.TryGetValue(node.GetType(), out GetControl))
                    {
                        newListControls.Add(GetControl(out controlHeight, out controlWidth, node, controlHeight, Width - left));
                    }
                    break;

                case (9):
                    newListControls.Add(GetTextControl(out controlHeight, node, Width - left));
                    break;
            }

            newListControls.AddRange(listControls);

            return newListControls;
        }


        private static BrowserControl GetTextControl(out double height, Node node, double width)
        {
            var label = new Label();
            label.Width = width;
            label.Text = node.NodeValue;
            height = label.Height;

            return label;
        }


        #region GetElementControl

        private static BrowserControl GetBodyControl(
            out double controlsWidth,
            out double controlsHeight,
            Node node,
            double left,
            double top
            )
        {
            controlsWidth = 0;
            controlsHeight = 0;
            return new Rectangle();
        }

        // private static BrowserControl GetButtonControl(out double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        // private static BrowserControl GetDivControl(out double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        // private static BrowserControl GetFormControl(out double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        // private static BrowserControl GetHeadingControl(out double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        private static BrowserControl GetParagraphControl(
            out double controlsWidth,
            out double controlsHeight,
            Node node,
            double left,
            double top
            )
        {
            var label = new Label();
            label.Width = Width - left;
            label.Text = node.NodeValue;
            controlsWidth = label.Width;
            controlsHeight = label.Height;

            return label;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using HoustonBrowser.DOM;
using HoustonBrowser.Controls;
using Avalonia.Media;

namespace HoustonBrowser.Render
{
    internal static class Control
    {
        enum TypeOfNode
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

        private delegate BrowserControl GetControl(
            NodeOfRenderTree renderNode,
            Node node
        );

        private static Dictionary<Type, GetControl> ControlsDictionary
            = new Dictionary<Type, GetControl>
            {
                [typeof(HTMLBodyElement)] = GetBodyControl,
                //[typeof(HTMLButtonElement)] = GetButtonControl,
                [typeof(HTMLDivElement)] = GetDivControl,
                //[typeof(HTMLFormElement)] = GetFormControl,
                //[typeof(HTMLH1Element)] = GetHeadingControl,
                //[typeof(HTMLH2Element)] = GetHeadingControl,
                //[typeof(HTMLH3Element)] = GetHeadingControl,
                //[typeof(HTMLH4Element)] = GetHeadingControl,
                //[typeof(HTMLH5Element)] = GetHeadingControl,
                //[typeof(HTMLH6Element)] = GetHeadingControl,
                [typeof(HTMLParagraphElement)] = GetParagraphControl,
                [typeof(Text)] = GetTextControl,
            };

        public static bool IsNodeRender(Node node)
        {
            switch (node.NodeType)
            {
                case ((int)TypeOfNode.ELEMENT_NODE):
                    if (node.NodeName == "head") return false;
                    return true;
                case (9):
                    return true;
                default:
                    return false;
            }
        }

        public static BrowserControl Get(
            NodeOfRenderTree renderNode,
            Node node
        )
        {
            GetControl control;
            if (ControlsDictionary.TryGetValue(node.GetType(), out control))
            {
                return control(renderNode, node);
            }
            else
            {
                Console.WriteLine("ZadError");
                return null;
            }
        }

        public static BrowserControl GetTextControl(
            NodeOfRenderTree renderNode,
            Node node
            )
        {
            renderNode.IsFixedSize = true;
            var label = new Label();
            label.Width = renderNode.Width;
            label.Left = renderNode.Left;
            label.Top = renderNode.Top;
            label.WrapText = TextWrapping.Wrap;
            label.Text = node.NodeValue;
            
            return label;
        }

        public static BrowserControl GetBodyControl(
            NodeOfRenderTree renderNode,
            Node node
            )
        {
            return new Rectangle()
            {
                Height = renderNode.Height - 10,
                Width = renderNode.Width - 20,
                Left = renderNode.Left + 10,
                Top = renderNode.Top + 10,
                BackgroundBrush = new SolidColorBrush(new Color(255, 100, 100, 0))
            };
        }

        // public static BrowserControl GetButtonControl(ref double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        public static BrowserControl GetDivControl(
            NodeOfRenderTree renderNode,
            Node node
        )
        {
            return new BrowserControl();
        }

        // public static BrowserControl GetFormControl(ref double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        // private static BrowserControl GetHeadingControl(ref double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }

        public static BrowserControl GetParagraphControl(
            NodeOfRenderTree renderNode,
            Node node
            )
        {

            return new Rectangle()
            {
                Width = renderNode.Width - 40,
                Left = renderNode.Left + 10,
                Top = renderNode.Top + 10,
                BackgroundBrush = new SolidColorBrush(new Color(255, 0, 0, 200))
            };
        }


        // set size of contrals

        private delegate void GetSizeControl(
            double inWidth,
            double inHeight,
            NodeOfRenderTree renderNode);

        private static Dictionary<Type, GetSizeControl> SizeControlsDictionary
            = new Dictionary<Type, GetSizeControl>
            {
                [typeof(HTMLBodyElement)] = GetSizeBodyControl,
                [typeof(HTMLDivElement)] = GetSizeDivControl,
                [typeof(HTMLParagraphElement)] = GetSizeParagraphControl,
            };

        public static void GetSize(
            double inWidth,
            double inHeight,
            NodeOfRenderTree renderNode)
        {
            GetSizeControl control;
            if (SizeControlsDictionary.TryGetValue(renderNode.NodeOfDom.GetType(), out control))
            {
                control(inWidth, inHeight, renderNode);
            }
        }

        public static void GetSizeParagraphControl(
            double inWidth,
            double inHeight,
            NodeOfRenderTree renderNode)
        {
            double marStart = 10;
            double marEnd = 10;

            renderNode.HeightControl = marStart + marEnd + inHeight;
        }

        public static void GetSizeBodyControl(
            double inWidth,
            double inHeight,
            NodeOfRenderTree renderNode)
        {
            if (renderNode.HeightControl < inHeight)
            {
                double marStart = 10;
                double marEnd = 10;

                renderNode.HeightControl = marStart + marEnd + inHeight;
            }
        }

        public static void GetSizeDivControl
            (
            double inWidth,
            double inHeight,
            NodeOfRenderTree renderNode
            )
        {

        }
    }
}

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
            ref Style style,
            RenderObj renderNode,
            Node node
        );

        private static Dictionary<Type, GetControl> ControlsDictionary
            = new Dictionary<Type, GetControl>
            {
                [typeof(HTMLBodyElement)] = GetBodyControl,
                [typeof(HTMLButtonElement)] = GetButtonControl,
                [typeof(HTMLDivElement)] = GetDivControl,
                //[typeof(HTMLFormElement)] = GetFormControl,
                [typeof(HTMLH1Element)] = GetH1Control,
                [typeof(HTMLH2Element)] = GetH2Control,
                [typeof(HTMLH3Element)] = GetH3Control,
                [typeof(HTMLH4Element)] = GetH4Control,
                [typeof(HTMLH5Element)] = GetH5Control,
                [typeof(HTMLH6Element)] = GetH6Control,
                [typeof(HTMLParagraphElement)] = GetParagraphControl,
                [typeof(Text)] = GetTextControl,
            };

        public static bool IsNodeRender(Node node)
        {
            switch (node.NodeType)
            {
                case ((int)TypeOfNode.ELEMENT_NODE):
                    if (node.NodeName == "head") return false;
                    if (node.NodeName == "a") return false;
                    if (node.NodeName == "link") return false;
                    if (node.NodeName == "img") return false;
                    if (node.NodeName == "hr") return false;
                    if (node.NodeName == "script") return false;
                    return true;
                case (9):
                    return true;
                default:
                    return false;
            }
        }


        // private static BrowserControl GetHeadingControl(ref double height, Node node, double width)
        // {
        //     return new BrowserControl();
        // }


        public static BrowserControl GetH1Control(
            ref Style style,
            RenderObj renderNode,
            Node node
        )
        {

            renderNode.IsFixedSize = true;
            style = new Style(10, 10, 0, 0);
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.FirstChild.NodeValue,
                TextTypeface = new Typeface("Arial", 10,FontStyle.Normal,FontWeight.Bold)
            };

            return control;
        }

        public static BrowserControl GetH2Control(
            ref Style style,
            RenderObj renderNode,
            Node node
        )
        {
            renderNode.IsFixedSize = true;
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.FirstChild.NodeValue,
                TextTypeface = new Typeface("Arial", 12,FontStyle.Normal, FontWeight.Bold)
            };

            style = new Style(10, 10, 0, 0);

            return control;
        }

        public static BrowserControl GetH3Control(ref Style style, RenderObj renderNode, Node node
)
        {
            renderNode.IsFixedSize = true;
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.FirstChild.NodeValue,
                TextTypeface = new Typeface("Arial", 14, FontStyle.Normal, FontWeight.Bold)
        };

            style = new Style(10, 10, 0, 0);

            return control;
        }


        public static BrowserControl GetH4Control(ref Style style, RenderObj renderNode, Node node
)
        {
            renderNode.IsFixedSize = true;
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.FirstChild.NodeValue,
                TextTypeface = new Typeface("Arial", 16, FontStyle.Normal, FontWeight.Bold)
            };

            style = new Style(10, 10, 0, 0);

            return control;
        }

        public static BrowserControl GetH5Control(ref Style style, RenderObj renderNode, Node node
)
        {
            renderNode.IsFixedSize = true;
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.FirstChild.NodeValue,
                TextTypeface = new Typeface("Arial", 18, FontStyle.Normal, FontWeight.Bold)
            };

            style = new Style(10, 10, 0, 0);

            return control;
        }

        public static BrowserControl GetH6Control(ref Style style, RenderObj renderNode, Node node
)
        {
            renderNode.IsFixedSize = true;
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.FirstChild.NodeValue,
                TextTypeface = new Typeface("Arial", 20, FontStyle.Normal, FontWeight.Bold)
            };

            style = new Style(10, 10, 0, 0);

            return control;
        }

        public static BrowserControl Get(
            ref Style style,
            RenderObj renderNode,
            Node node
        )
        {
            GetControl control;
            if (ControlsDictionary.TryGetValue(node.GetType(), out control))
            {
                return control(ref style, renderNode, node);
            }

            Console.WriteLine("Zad");
            style = new Style(0,0,0,0);
            return null;
        }

        public static BrowserControl GetTextControl(
            ref Style style,
            RenderObj renderNode,
            Node node
            )
        {
            renderNode.IsFixedSize = true;
            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.NodeValue,
              //  BackgroundBrush=new SolidColorBrush(new Color(255, 250, 0, 0))
            };

            style = new Style(0, 0, 0, 0);

            return control;
        }

        public static BrowserControl GetBodyControl(
            ref Style style,
            RenderObj renderNode,
            Node node
            )
        {
            var control =  new Controls.Rectangle()
            {
              //  BackgroundBrush = new SolidColorBrush(new Color(255, 100, 100, 0))
            };
            style = new Style(10, 10, 10, 10);

            return control;
        }

        public static BrowserControl GetButtonControl(
            ref Style style,
            RenderObj renderNode,
            Node node
        )
        {
            style = new Style(0, 0, 0, 0);

            return new Controls.Button()
            { Text = node.NodeValue,
                Width = 120,
            };
        }

        public static BrowserControl GetDivControl(
            ref Style style,
            RenderObj renderNode,
            Node node
        )
        {
            style = new Style(0, 0, 0, 0);

            return new Controls.Rectangle();
        }

        public static BrowserControl GetParagraphControl(
            ref Style style,
            RenderObj renderNode,
            Node node
            )
        {
            var control = new Controls.Rectangle()
            {
                Height = 5,
               // BackgroundBrush = new SolidColorBrush(new Color(255, 0, 0, 200))
            };

            style = new Style(15, 15, 0, 0);

            return control;
        }
    }
}

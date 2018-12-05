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
            RenderObj renderNode,
            Node node
        );

        private static Dictionary<Type, GetControl> ControlsDictionary
            = new Dictionary<Type, GetControl>
            {
                [typeof(HTMLBodyElement)] = GetBodyControl,
                //[typeof(HTMLButtonElement)] = GetButtonControl,
                [typeof(HTMLDivElement)] = GetDivControl,
                //[typeof(HTMLFormElement)] = GetFormControl,
                [typeof(HTMLH1Element)] = GetH1Control,
                [typeof(HTMLH2Element)] = GetH2Control,
                [typeof(HTMLH3Element)] = GetH3Control,
                [typeof(HTMLH4Element)] = GetH4Control,
                [typeof(HTMLH5Element)] = GetH5Control,
                [typeof(HTMLH6Element)] = GetH6Control,
                [typeof(HTMLIElement)] = GetIControl,
                [typeof(HTMLParagraphElement)] = GetParagraphControl,
                [typeof(Text)] = GetTextControl,
            };

        public static bool IsNodeRender(Node node)
        {
            return ControlsDictionary.ContainsKey(node.GetType());
        }


        public static BrowserControl Get(RenderObj renderNode, Node node)
        {
            GetControl control;
            if (ControlsDictionary.TryGetValue(node.GetType(), out control))
            {
                return control(renderNode, node);
            }

            return null;
        }

        public static BrowserControl GetTextControl(RenderObj renderNode, Node node)
        {
            renderNode.IsFixedSize = true;
            renderNode.Style = new Style(0, 0, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = renderNode.PreviousNode.Style.SizeFont,
                Bold = renderNode.PreviousNode.Style.Bold,
                FontStyle = renderNode.PreviousNode.Style.FontStyle,
            };

            var control = new Label()
            {
                Width = renderNode.Width,
                WrapText = TextWrapping.Wrap,
                Text = node.NodeValue,
                // BackgroundBrush = new SolidColorBrush(new Color(200, 250, 0, 0))
            };

            if (renderNode.Style.Bold)
                control.TextTypeface = new Typeface(renderNode.Style.Font, renderNode.Style.SizeFont, renderNode.Style.FontStyle, FontWeight.Bold);
            else
                control.TextTypeface = new Typeface(renderNode.Style.Font, renderNode.Style.SizeFont, renderNode.Style.FontStyle, FontWeight.Normal);

            return control;
        }

        public static BrowserControl GetBodyControl(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(10, 10, 10, 10);

            var control = new Controls.Rectangle()
            {
                // BackgroundBrush = new SolidColorBrush(new Color(255, 100, 100, 0))
            };

            return control;
        }

        public static BrowserControl GetButtonControl(RenderObj renderNode, Node node)
        {
            renderNode.IsFixedSize = true;
            renderNode.Style = new Style(0, 0, 0, 0)
            {
                Font = renderNode.Style.Font,
                SizeFont = renderNode.Style.SizeFont,
                Bold = renderNode.Style.Bold,
            };

            return new Controls.Button()
            {
                Text = node.NodeValue,
                Width = 30
            };
        }

        public static BrowserControl GetDivControl(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(0, 0, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = renderNode.PreviousNode.Style.SizeFont,
                Bold = renderNode.PreviousNode.Style.Bold,
            };

            var control = new Controls.Rectangle()
            {
                Height = 1,
                Width = renderNode.Width,
             //   BackgroundBrush = new SolidColorBrush(new Color(255, 0, 0, 200))
            };

            return control;
        }

        public static BrowserControl GetParagraphControl(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(15, 15, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = renderNode.PreviousNode.Style.SizeFont,
                Bold = renderNode.PreviousNode.Style.Bold,
            };

            var control = new Controls.Rectangle()
            {
                Height = 1,

                // BackgroundBrush = new SolidColorBrush(new Color(255, 0, 100, 100))
            };

            return control;
        }

        public static BrowserControl GetIControl(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(0, 0, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = renderNode.PreviousNode.Style.SizeFont,
                Bold = renderNode.PreviousNode.Style.Bold,
                FontStyle = FontStyle.Italic
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }

        public static BrowserControl GetH1Control(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(17, 17, 0, 0)
            {
                // Font = renderNode.PreviousNode.Style.Font,
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = 30,
                Bold = true,
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }

        public static BrowserControl GetH2Control(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(13, 13, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = 24,
                Bold = true,
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }

        public static BrowserControl GetH3Control(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(11, 11, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = 20,
                Bold = true,
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }


        public static BrowserControl GetH4Control(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(10, 10, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = 15,
                Bold = true,
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }

        public static BrowserControl GetH5Control(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(10, 10, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = 12,
                Bold = true,
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }

        public static BrowserControl GetH6Control(RenderObj renderNode, Node node)
        {
            renderNode.Style = new Style(10, 10, 0, 0)
            {
                Font = renderNode.PreviousNode.Style.Font,
                SizeFont = 10,
                Bold = true,
            };

            return new Controls.Rectangle()
            {
                Height = 1,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class RenderPage
    {
        private HTMLDocument document;
        private RenderTree renderTree;

        public static double Width { get; set; } = 920;
        public double Height { get; set; } = 500;
        public List<BrowserControl> ListOfControls { get => renderTree.ListOfControls;  }

        public RenderPage(HTMLDocument document)
        {
            this.document = document;

            foreach (Node node in document.FirstChild.ChildNodes)
            {
                if (node.GetType() == typeof(HTMLBodyElement))
                {
                    renderTree = new RenderTree(node);
                    renderTree.Width = Width;
                    renderTree.Height = Height;
                    renderTree.Left = 0;
                    renderTree.Top = 0;

                    BuildRenderTree(node, renderTree);
                    renderTree.Relayout();
                }
            }
        }


        public void BuildRenderTree(Node node, RenderObj RenderObj)
        {
            RenderObj newRenderObj;

            if (node.ChildNodes.Count != 0)
            {
                foreach (Node childNode in node.ChildNodes)
                {
                    if (Control.IsNodeRender(childNode))
                    {
                        newRenderObj = new RenderObj(
                        RenderObj,
                        renderTree,
                        childNode
                        );
                        RenderObj.Childs.Add(newRenderObj);

                        BuildRenderTree(childNode, newRenderObj);
                    }
                    else
                        BuildRenderTree(childNode, RenderObj);
                }
            }
        }
    }
}

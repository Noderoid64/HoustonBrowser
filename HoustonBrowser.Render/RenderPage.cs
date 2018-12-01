
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
        public List<BrowserControl> ListOfControls { get => renderTree.GetListOfControls();  }

        public RenderPage(HTMLDocument document)
        {
            this.document = document;
            var bodyNode = document.GetElementsByTagName("body");

            if (bodyNode.Count != 0)
            {
                renderTree = new RenderTree(bodyNode[0], Width, Height);
                BuildRenderTree(bodyNode[0], renderTree);
                renderTree.Relayout();
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
                        RenderObj.AppendChild(newRenderObj);

                        BuildRenderTree(childNode, newRenderObj);
                    }
                    else
                        BuildRenderTree(childNode, RenderObj);
                }
            }
        }
    }
}

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
        public double Height { get; set; } = 490;
        public List<BrowserControl> ListOfControls { get => renderTree.ListOfControls;  }

        public RenderPage(HTMLDocument document)
        {
            this.document = document;
            renderTree = new RenderTree();
            renderTree.Width = Width;
            renderTree.Height = Height;
            renderTree.Left = 0;
            renderTree.Top = 0;
            BuildRenderTree(document, renderTree);
            renderTree.Relayout();
        }

        public void BuildRenderTree(Node node, NodeOfRenderTree nodeOfRenderTree)
        {
            NodeOfRenderTree newNodeOfRenderTree;

            if (node.ChildNodes.Count != 0)
            {
                foreach (Node childNode in node.ChildNodes)
                {
                    if (Control.IsNodeRender(childNode))
                    {
                        if (nodeOfRenderTree.ControlOfThisNode == null)
                        {
                            nodeOfRenderTree.AddControl(childNode);
                            BuildRenderTree(childNode, nodeOfRenderTree);
                        }
                        else
                        {
                            newNodeOfRenderTree = new NodeOfRenderTree(
                            nodeOfRenderTree,
                            renderTree,
                            childNode
                            );
                            nodeOfRenderTree.Childs.Add(newNodeOfRenderTree);

                            BuildRenderTree(childNode, newNodeOfRenderTree);
                        }
                    }
                    else
                        BuildRenderTree(childNode, nodeOfRenderTree);
                }
            }
        }
    }
}

using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class NodeOfRenderTree
    {
        protected NodeOfRenderTree previousNode;
        protected RenderTree rootNode;

        Node nodeOfDom;
        public Node NodeOfDom { get => nodeOfDom; }
        public bool IsFixedSize { get; set; } = false;
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double LeftControl
        {
            get => ControlOfThisNode.Left;
            set => ControlOfThisNode.Left = value;
        }
        public double TopControl
        {
            get => ControlOfThisNode.Top;
            set => ControlOfThisNode.Top = value;
        }
        public double WidthControl
        {
            get => ControlOfThisNode.Width;
            set => ControlOfThisNode.Width = value;
        }
        public double HeightControl
        {
            get => ControlOfThisNode.Height;
            set => ControlOfThisNode.Height = value;
        }

        public double LeftBlock { get; set; }
        public double TopBlock { get; set; }
        public double WidthBlock { get; set; }
        public double HeightBlock { get; set; }

        public List<NodeOfRenderTree> Childs { get; set; }
             = new List<NodeOfRenderTree>();
        public NodeOfRenderTree PreviousNode { get => previousNode; }
        public BrowserControl ControlOfThisNode { get; set; }

        protected NodeOfRenderTree(
            NodeOfRenderTree previousNode,
            RenderTree rootNode)
        {
            this.previousNode = previousNode;
            this.rootNode = rootNode; 
        }

        public NodeOfRenderTree(
            NodeOfRenderTree previousNode,
            RenderTree rootNode,
            Node node)
        {
            nodeOfDom = node;
            this.previousNode = previousNode;
            this.rootNode = rootNode;
            Width = previousNode.WidthControl;
            Left = previousNode.LeftControl;
            Top = previousNode.TopControl;
            ControlOfThisNode = Control.Get(this, node);
        }

        protected List<BrowserControl> GetListOfControls(NodeOfRenderTree nodeOfRenderTree)
        {
            var tmpList = new List<BrowserControl>();
            tmpList.Add(nodeOfRenderTree.ControlOfThisNode);

            foreach (NodeOfRenderTree node in nodeOfRenderTree.Childs)
            {
                tmpList.AddRange(GetListOfControls(node));
            }

            return tmpList;
        }

        public void AddControl(Node node)
        {
            ControlOfThisNode = Control.Get(this, node);
            nodeOfDom = node;
        }

        public void Relayout(double localWidth, double localHeigth)
        {
            if (!IsFixedSize && Childs.Count != 0)
            {
                double localLeft = LeftBlock;
                double localTop = TopBlock;

                foreach (NodeOfRenderTree node in Childs)
                {
                    node.Relayout();

                    if ((localWidth + node.Width) < WidthBlock)
                    {
                        node.Left = localLeft;
                        node.Top = localTop;

                        localWidth += node.Width;
                        localLeft += node.Width;
                        localHeigth = node.Height;
                    }
                    else
                    {
                        localTop += localHeigth;
                        node.Top = localTop;
                        node.Left = localLeft;
                        localWidth = 0;
                        localLeft = LeftBlock;
                        localHeigth += node.Height;

                        
                        
                    }
                }
            }
        }
    }
}
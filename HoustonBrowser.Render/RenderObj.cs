using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class RenderObj
    {
        protected RenderObj previousNode;
        protected RenderTree rootNode;

        public Style style;
        Node nodeOfDom;
        public Node NodeOfDom { get => nodeOfDom; }
        public bool IsFixedSize { get; set; } = false;

        double left;
        double top;
        double width;
        public double height;

        public double Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
                ControlRenderObj.Left = value + style.DistanceBetweenControl.Left;
            }
        }

        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
                ControlRenderObj.Top = value + style.DistanceBetweenControl.Top;
            }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                ControlRenderObj.Width = value - style.DistanceBetweenControl.Width;
            }
        }

        public double Height
        {
            get
            {
                return style.DistanceBetweenControl.Height + ControlRenderObj.Height;
            }
            set
            {
                height = value;
                ControlRenderObj.Height = value - style.DistanceBetweenControl.Height;
            }
        }

        public double LeftBlock
        {
            get
            {
                return ControlRenderObj.Left + style.DistanceBetweenBlock.Left;
            }
        }

        public double TopBlock
        {
            get
            {
                return ControlRenderObj.Top + style.DistanceBetweenBlock.Top;
            }
        }

        public double WidthBlock
        {
            get
            {
                return ControlRenderObj.Width - style.DistanceBetweenBlock.Width;
            }
        }

        public double HeightBlock
        {
            get
            {
                return ControlRenderObj.Height - style.DistanceBetweenBlock.Height;
            }
        }

        public List<RenderObj> Childs { get; set; }
             = new List<RenderObj>();
        public RenderObj PreviousNode { get => previousNode; }
        public BrowserControl ControlRenderObj { get; set; }

        protected RenderObj(Node node)
        {
            nodeOfDom = node;
            ControlRenderObj = Control.Get(ref style, this, node);
            this.previousNode = null;
            this.rootNode = null;
        }

        public RenderObj(
            RenderObj previousNode,
            RenderTree rootNode,
            Node node)
        {
            nodeOfDom = node;
            this.previousNode = previousNode;
            this.rootNode = rootNode;

            ControlRenderObj = Control.Get(ref style, this, node);
            Left = previousNode.LeftBlock;
            Top = previousNode.TopBlock;
            Width = previousNode.WidthBlock;
        }

        public void ReLocation()
        {
            if (Childs.Count != 0)
            {
                foreach (RenderObj obj in Childs)
                {
                    obj.Left = LeftBlock;
                    obj.Top = TopBlock;

                    obj.ReLocation();
                }
            }
        }

        public void Relayout()
        {
            if (!IsFixedSize)
            {
                double localLeft = LeftBlock;
                double localTop = TopBlock;
                double localWidth = 0;
                double localHeight = 0;

                foreach (RenderObj obj in Childs)
                {
                    obj.Relayout();

                    if ((obj.Width + localWidth) < WidthBlock)
                    {
                        obj.Left = localLeft;
                        obj.Top = localTop;

                        localLeft += obj.Width;
                        localHeight = obj.Height;
                    }
                    else
                    {
                        localLeft = LeftBlock;
                        obj.Top = localTop;
                        localTop += obj.Height;
                        obj.Left = localLeft;    
                        localHeight = obj.Height;
                    }

                    obj.ReLocation();
                }
            }
        }

        protected List<BrowserControl> GetListOfControls(RenderObj RenderObj)
        {
            var tmpList = new List<BrowserControl>();
            tmpList.Add(RenderObj.ControlRenderObj);

            foreach (RenderObj node in RenderObj.Childs)
            {
                tmpList.AddRange(GetListOfControls(node));
            }

            return tmpList;
        }
    }
}
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class RenderObj
    {
        protected RenderObj previousObj;
        protected RenderTree rootObj;

        public Style Style { get; set; }
        public Node nodeOfDom;
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
                ControlRenderObj.Left = value + Style.DistanceBetweenControl.Left;
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
                ControlRenderObj.Top = value + Style.DistanceBetweenControl.Top;
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
                ControlRenderObj.Width = value - Style.DistanceBetweenControl.Width;
            }
        }

        public double Height
        {
            get
            {
                return Style.DistanceBetweenControl.Height + ControlRenderObj.Height;
            }
            set
            {
                height = value;
                ControlRenderObj.Height = value - Style.DistanceBetweenControl.Height;
            }
        }

        public double LeftBlock
        {
            get
            {
                return ControlRenderObj.Left + Style.DistanceBetweenBlock.Left;
            }
        }

        public double TopBlock
        {
            get
            {
                return ControlRenderObj.Top + Style.DistanceBetweenBlock.Top;
            }
        }

        public double WidthBlock
        {
            get
            {
                return ControlRenderObj.Width - Style.DistanceBetweenBlock.Width;
            }
        }

        public double HeightBlock
        {
            get
            {
                return ControlRenderObj.Height - Style.DistanceBetweenBlock.Height;
            }
        }

        public List<RenderObj> Childs { get; set; } = new List<RenderObj>();
        public RenderObj PreviousNode { get => previousObj; }
        public BrowserControl ControlRenderObj { get; set; }

        public RenderObj(Node node)
        {
            nodeOfDom = node;
        }

        public RenderObj(
            RenderObj previousObj,
            RenderTree rootObj,
            Node node)
        {
            nodeOfDom = node;
            this.previousObj = previousObj;
            this.rootObj = rootObj;

            ControlRenderObj = Control.Get(this, node);
            Left = previousObj.LeftBlock;
            Top = previousObj.TopBlock;
            if (!IsFixedSize)
                Width = previousObj.WidthBlock;
        }

        public void ReLocation(double movingLeft, double movingTop)
        {
            if (Childs.Count != 0)
            {
                double localLeft;
                double localTop = 0;
                foreach (RenderObj obj in Childs)
                {
                    obj.Left = LeftBlock;
                    obj.Top = TopBlock + localTop;
                    localTop += obj.Height;

                    //obj.Left = LeftBlock + movingLeft;
                    //obj.Top = TopBlock + movingTop;

                    obj.ReLocation(movingLeft, movingTop);
                }
            }
        }

        public void Relayout()
        {
            if (!IsFixedSize && (Childs.Count != 0))
            {
                double localLeft = LeftBlock;
                double localTop = TopBlock;
                double localWidth = 0;
                double localHeight = 0;
                double prevHeight = 0;

                foreach (RenderObj obj in Childs)
                {
                    obj.Relayout();

                    double objLeft = obj.Left;
                    double objTop = obj.Top;

                    if ((obj.Width + localWidth) < WidthBlock)
                    {
                        obj.Left = localLeft;
                        obj.Top = localTop;

                        localLeft += obj.Width;
                        localWidth += obj.Width;
                        
                        if (localHeight == 0)
                        {
                            localHeight += obj.Height;
                        }
                        else
                        {
                            if (prevHeight < obj.Height)
                                localHeight += (obj.Height - prevHeight);
                        }

                        prevHeight = obj.Height;
                    }
                    else
                    {
                        localLeft = LeftBlock;
                        localWidth = 0;
                        localWidth += obj.Width;

                        obj.Left = LeftBlock;
                        localTop = TopBlock + localHeight;
                        obj.Top = localTop;

                        localHeight += obj.Height;
                    }


                    obj.ReLocation(obj.Left - objLeft, obj.Top - objTop);
                }

                ControlRenderObj.Height = localHeight + Style.DistanceBetweenBlock.Height;
            }
        }

        public List<BrowserControl> GetListOfControls(RenderObj RenderObj)
        {
            var tmpList = new List<BrowserControl>();
            tmpList.Add(RenderObj.ControlRenderObj);

            foreach (RenderObj node in RenderObj.Childs)
            {
                tmpList.AddRange(GetListOfControls(node));
            }

            return tmpList;
        }

        public void AppendChild(RenderObj child)
        {
            Height += child.Height;
            Childs.Add(child);
        }
    }
}
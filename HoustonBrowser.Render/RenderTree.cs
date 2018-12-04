using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class RenderTree : RenderObj
    {
        public RenderTree(Node node, double width, double height) : base(node)
        {
            ControlRenderObj = Control.GetBodyControl(ref style, this, node);
            Width = width;
            Left = 0;
            Top = 0;
        }

        public List<BrowserControl> GetListOfControls()
        {
            var tmpList = new List<BrowserControl>();
            tmpList.Add(ControlRenderObj);
            foreach (RenderObj node in Childs)
            {
                tmpList.AddRange(GetListOfControls(node));
            }

            return tmpList;
        }
    }
}
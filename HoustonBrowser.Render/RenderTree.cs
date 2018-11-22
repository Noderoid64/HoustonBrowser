using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public class RenderTree : RenderObj
    {
        private List<BrowserControl> listOfControls = new List<BrowserControl>();

        public List<BrowserControl> ListOfControls
        {
            get {
                if (listOfControls.Count == 0)
                    listOfControls = GetListOfControls();

                return listOfControls;
            }

        }

        public RenderTree(Node node) : base(node) { }

        private List<BrowserControl> GetListOfControls()
        {
            var tmpList = new List<BrowserControl>();
            tmpList.Add(ControlRenderObj);
            foreach (RenderObj node in Childs)
            {
                tmpList.AddRange(GetListOfControls(node));
            }

            return tmpList;
        }

        public void Relayout1()
        {

        }
    }
}
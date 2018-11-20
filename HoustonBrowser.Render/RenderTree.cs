using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;

namespace HoustonBrowser.Render
{
    public class RenderTree : NodeOfRenderTree
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

        public RenderTree() : base(null, null) { }

        private List<BrowserControl> GetListOfControls()
        {
            var tmpList = new List<BrowserControl>();

            foreach (NodeOfRenderTree node in Childs)
            {
                tmpList.AddRange(GetListOfControls(node));
            }

            return tmpList;
        }
    }
}
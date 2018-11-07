using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;
using HoustonBrowser.DOM.HTML;


namespace HoustonBrowser.Render
{
    public class RenderTree
    {
        private HTMLDocument document;

        public RenderTree(HTMLDocument document)
        {
            this.document = document;
        }

        public List<BrowserControl> GetPage()
        {
            var listControls = new List<BrowserControl>();

            return listControls;
        }
    }
}

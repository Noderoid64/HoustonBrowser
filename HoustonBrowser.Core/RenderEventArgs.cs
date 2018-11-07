using System;
using System.Collections.Generic;
using System.Text;
using HoustonBrowser.Controls;

namespace HoustonBrowser.Core
{
    public class RenderEventArgs: EventArgs
    {
        
        public List<BrowserControl> Page { get; set; }
        
        public RenderEventArgs(List<BrowserControl> page)
        {
            Page=page;
        }
    }
}

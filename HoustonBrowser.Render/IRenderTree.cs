using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public interface IRenderTree
    {
        List<BrowserControl> GetPage();
        
    }
}

using System;
using System.Collections.Generic;
using HoustonBrowser.Controls;
using HoustonBrowser.DOM;

namespace HoustonBrowser.Render
{
    public interface IRenderPage
    {
        List<BrowserControl> GetPage();
        
    }
}

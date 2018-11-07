using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Core
{
    public class RenderEventArgs: EventArgs
    {
        
        public string Data { get; set; }
        
        public RenderEventArgs(string data)
        {
            Data = data;
        }
    }
}

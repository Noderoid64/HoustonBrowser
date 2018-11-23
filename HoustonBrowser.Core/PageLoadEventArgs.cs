using System;

namespace HoustonBrowser.Core
{
    public class PageLoadEventArgs: EventArgs
    {
        public string UrlString { get; set; }


        public PageLoadEventArgs(string url)
        {
            UrlString=url;
        }



    }
}
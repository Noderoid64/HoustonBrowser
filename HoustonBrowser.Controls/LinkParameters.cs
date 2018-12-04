using System;

namespace HoustonBrowser.Controls
{
    public class LinkParameters
    {
        public int StartIndex {get;set;}
        public int EndIndex {get;set;}
        public string URL {get;set;}

        public bool IsPressed {get;set;}

        public LinkParameters(int startIndex, int endIndex, string url)
        {
            StartIndex=startIndex;
            EndIndex=endIndex;
            URL=url;
            IsPressed=false;
        }
    } 
}
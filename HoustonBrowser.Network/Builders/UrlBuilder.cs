using System;

namespace HoustonBrowser.HttpModule.Builders
{
    public static class UrlBuilder
    {
        public static string GetHost(string Url)
        {
            if(Url.StartsWith("http://"))
            Url = Url.Replace("http://","");
            if(Url.StartsWith("https://"))
            Url = Url.Replace("https://","");
            if(Url.Contains('/'))
            return Url.Substring(0,Url.IndexOf('/'));
            else
            return Url;            
        }
        public static string GetRequestUri(string Url)
        {
            if(Url.StartsWith("http://"))
            Url = Url.Replace("http://","");
            if(Url.StartsWith("https://"))
            Url = Url.Replace("https://","");
            if(Url.Contains('/'))
            return Url.Substring(Url.IndexOf('/'));
            else
            return null;
        }
    }
}
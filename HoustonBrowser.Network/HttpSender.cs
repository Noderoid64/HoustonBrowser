using System;
using System.Threading.Tasks;
using System.Threading;

namespace HoustonBrowser.HttpModule
{
    public class HttpSender
    {
        public event Action<string> GetHttpHtmlResponce;
        public event Action<string> GetHttmCssResponce;
        public event Action<string> GetHttmJsResponce;

        public async void SendHttpHtmlRequest(string host)
        {
            
        }
        public async void SendHttpCssRequest(string host)
        {
            
        }
        public async void SendHttpJsRequest(string host)
        {
            
        }

        
    }
}
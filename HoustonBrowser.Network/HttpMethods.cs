using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule
{
    internal static class HttpMethod
    {
        public static HttpMethods FromString(this HttpMethods m, string value)
        {
            if(value == HttpMethods.GET.ToString())
            return HttpMethods.GET;
            else if(value == HttpMethods.POST.ToString())
            return HttpMethods.POST;
            else if(value == HttpMethods.CONNECT.ToString())
            return HttpMethods.CONNECT;
            else if(value == HttpMethods.DELETE.ToString())
            return HttpMethods.DELETE;
            else if(value == HttpMethods.HEAD.ToString())
            return HttpMethods.HEAD;
            else if(value == HttpMethods.OPTIONS.ToString())
            return HttpMethods.OPTIONS;
            else if(value == HttpMethods.PATCH.ToString())
            return HttpMethods.PATCH;
            else if(value == HttpMethods.PUT.ToString())
            return HttpMethods.PUT;
            else if(value == HttpMethods.TRACE.ToString())
            return HttpMethods.TRACE;
            else throw new Exception("Incorrect method value");
            
        }
    }
    internal enum HttpMethods
    {
        OPTIONS,
        GET,
        HEAD,
        POST,
        PUT,
        PATCH,
        DELETE,
        TRACE,
        CONNECT
    }
}

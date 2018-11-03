using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule
{
    internal static class HttpMethod
    {
        public static void FromString(this HttpMethods m, string value)
        {
            if(value == HttpMethods.GET.ToString())
            m = HttpMethods.GET;
            else if(value == HttpMethods.POST.ToString())
            m = HttpMethods.POST;
            else if(value == HttpMethods.CONNECT.ToString())
            m = HttpMethods.CONNECT;
            else if(value == HttpMethods.DELETE.ToString())
            m = HttpMethods.DELETE;
            else if(value == HttpMethods.HEAD.ToString())
            m = HttpMethods.HEAD;
            else if(value == HttpMethods.OPTIONS.ToString())
            m = HttpMethods.OPTIONS;
            else if(value == HttpMethods.PATCH.ToString())
            m = HttpMethods.PATCH;
            else if(value == HttpMethods.PUT.ToString())
            m = HttpMethods.PUT;
            else if(value == HttpMethods.TRACE.ToString())
            m = HttpMethods.TRACE;
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

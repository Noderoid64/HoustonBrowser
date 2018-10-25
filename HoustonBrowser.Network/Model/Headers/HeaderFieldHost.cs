using System;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HeaderFieldHost : HttpHeaderField
    {
        const string FieldName = "Host";
        public HeaderFieldHost(string host)
        {
            base.name = FieldName;
            base.value = host;
        }
        
    }
}
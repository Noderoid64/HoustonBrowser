using System;



namespace HoustonBrowser.HttpModule.Model.Headers
{
    internal class HeaderFieldHost : HttpHeaderField
    {
        public const string FieldName = "Host";
        public HeaderFieldHost(string host)
        {
            base.name = FieldName;
            base.value = FormatHost(host);
        }
        public string FormatHost(string host)
        {
            if (host.StartsWith("http://"))
                host = host.Replace("http://", "");
            return host;
        }
        #region IParseble
        public override void FromString(string value){
            base.name = value.Split(':')[0];
            base.value = (value.Split(':')[1]).Substring(2);
        }
        #endregion


    }
}
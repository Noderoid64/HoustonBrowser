using System;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HeaderFieldCacheControl : HttpHeaderField
    {
        public const string FieldName = "Cache-Control";
        public enum Params : byte { NoChache, NoStore, OnlyIfCached, MaxAge }
        /*
        Cache-Control: max-age=<seconds>
Cache-Control: max-stale[=<seconds>]
Cache-Control: min-fresh=<seconds>
Cache-Control: no-cache 
Cache-Control: no-store
Cache-Control: no-transform
Cache-Control: only-if-cached

Cache-Control: must-revalidate
Cache-Control: no-cache
Cache-Control: no-store
Cache-Control: no-transform
Cache-Control: public
Cache-Control: private
Cache-Control: proxy-revalidate
Cache-Control: max-age=<seconds>
Cache-Control: s-maxage=<seconds>
         */
        public HeaderFieldCacheControl(Params p, uint deltaSeconds = 1)
        {
            base.name = FieldName;
            base.value = GetStringByParam(p, deltaSeconds);
        }
        private string GetStringByParam(Params p, uint delta = 0)
        {
            switch (p)
            {
                case Params.NoChache:
                    {
                        return "no-cache";
                    }
                case Params.MaxAge:
                    {
                        return "max-age=" + delta;
                    }
                case Params.NoStore:
                    {
                        return "no-store";
                    }
                case Params.OnlyIfCached:
                    {
                        return "only-if-cached";
                    }
                default: return p.ToString();
            }
        }
        #region IParseble
        public override void FromString(string value)
        {

        }
        #endregion
    }
}
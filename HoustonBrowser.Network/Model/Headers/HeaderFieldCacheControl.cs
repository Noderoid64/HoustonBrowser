using System;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HeaderFieldCacheControl : HttpHeaderField
    {
        const string FieldName = "Cache-Control";
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
            switch (p)
            {
                case Params.NoChache:
                    {
                        value = "no-cache ";
                    }
                    break;
                case Params.MaxAge:
                    {
                        value = "max-age=" + deltaSeconds;
                    }
                    break;
                case Params.NoStore:
                    {
                        value = "no-store";
                    }
                    break;
                case Params.OnlyIfCached:
                    {
                        value = "only-if-cached";
                    }
                    break;
            }
        }

    }
}
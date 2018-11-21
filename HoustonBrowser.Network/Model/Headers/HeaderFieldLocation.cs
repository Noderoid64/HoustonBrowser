using System;

namespace HoustonBrowser.HttpModule.Model.Headers
{
    internal class HeaderFieldLocation : HttpHeaderField
    {
        public const string FieldName = "Location";

        public HeaderFieldLocation(string value)
        {
            value = value.RemoveSpaces();
            if (value.StartsWith(FieldName))
                value = value.Remove(0, FieldName.Length + 1);
                value = value.RemoveSpaces();
                base.value = value;
                base.name = FieldName;
        }
    }
}
using System;

namespace HoustonBrowser.HttpModule.Model.Headers
{
    internal class HeaderFieldContentType : HttpHeaderField
    {
        public const string FieldName = "Content-Type";
        public string[] values { get; set; }

        public HeaderFieldContentType(string value)
        {
            value = value.RemoveSpaces();
            if (value.StartsWith(FieldName))
            {
                name = value.Substring(0, FieldName.Length);
                value = value.Remove(0, FieldName.Length + 1);
            }


            values = value.Split(';');

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].EndsWith(';'))
                    values[i] = values[i].Remove(values[i].Length - 1, 1);
                values[i] = values[i].RemoveSpaces();
                base.value += values[i];
            }
        }
        
    }
}
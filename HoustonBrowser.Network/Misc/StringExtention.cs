using System;

namespace HoustonBrowser.HttpModule{
    internal static class StringExtention{
        public static string RemoveSpaces(this string value)
        {
            while (value.StartsWith(" "))
                value = value.Remove(0, 1);
            while (value.EndsWith(" "))
                value = value.Remove(value.Length - 1, 1);
            return value;
        }
    } 
}
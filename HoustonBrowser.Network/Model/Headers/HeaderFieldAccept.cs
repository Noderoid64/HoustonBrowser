using System;
using System.Runtime.CompilerServices;

namespace HoustonBrowser.HttpModule.Model
{
    internal class HeaderFieldAccept : HttpHeaderField
    {
        private const string FieldName = "Accept";
        public enum Type : int { any, text, image, application }
        public enum SubType : int { any, html, xhtmlXml, xml, xhtml, jpeg }
        public HeaderFieldAccept(Type type, SubType subType, float q = 1)
        {
            base.name = FieldName;

        }
        private string GetStringFromType(Type type)
        {
            switch (type)
            {
                case Type.any:
                    {
                        return "*";
                    }
                default:
                    return type.ToString();
            }
        }
        private string GetStringFromSubType(SubType subType)
        {
            switch (subType)
            {
                case SubType.any:
                    {
                        return "*";
                    }
                case SubType.xhtmlXml:
                    {
                        return "xhtml+xml";
                    }
                default:
                    {
                        return subType.ToString();
                    }
            }
        }

    }
}
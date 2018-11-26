namespace HoustonBrowser.JS
{
    static class TypeConverter
    {
        public static Primitive ToPrimitive(Primitive input, ESType preferredType = ESType.Undefined)
        {

            return input;
        }

        public static bool ToBoolean(Primitive input)
        {
            switch (input.Type)
            {
                case ESType.Undefined:
                case ESType.Null:
                    return false;
                case ESType.Object:
                    return true;

                case ESType.Boolean:
                    return (bool)input.Value;
                case ESType.String:
                    return ((string)input.Value).Length > 0;
                case ESType.Number:
                    break;

                default:
                    break;
            }

            return false;
        }

        public static bool ToNumber(Primitive input)
        {
            switch (input.Type)
            {
                case ESType.Undefined:
                case ESType.Null:
                    return false;
                case ESType.Object:
                    return true;

                case ESType.Boolean:
                    return (bool)input.Value;
                case ESType.String:
                    return ((string)input.Value).Length > 0;
                case ESType.Number:
                    break;

                default:
                    break;
            }

            return false;
        }

        public static string ToString(Primitive input) //not by spec see page 42
        {
            switch (input.Type)
            {
                case ESType.Undefined:
                    return "undefined";
                case ESType.Null:
                    return "null";
                case ESType.Boolean:
                    if ((bool)input.Value) return "true";
                    else return "false";
                case ESType.String:
                    return (string)input.Value;
                case ESType.Number:
                    return (string)input.Value;
                case ESType.Object:
                    return ToString(ToPrimitive(input, ESType.String));
                default:
                    break;
            }
            return "unkown";
        }
    }
}

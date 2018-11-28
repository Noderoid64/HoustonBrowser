using System;

namespace HoustonBrowser.JS
{
    static class TypeConverter
    {
        internal static Primitive ToPrimitive(Primitive input, ESType prefferedType)
        {
            switch (input.Type)
            {
                case ESType.Undefined:
                case ESType.Null:
                case ESType.Boolean:
                case ESType.String:
                case ESType.Number:
                    return input;

                case ESType.Object:
                    

                default:
                    break;
            }

            return null;
        }

        internal static bool ToBoolean(Primitive input)
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
                    double d = (double)input.Value;
                    if (d == 0 || double.IsNaN(d) || d == -double.Epsilon) return false;
                    return true;
                default:
                    break;
            }

            return false;
        }

        internal static double ToNumber(Primitive input)
        {
            switch (input.Type)
            {
                case ESType.Undefined:
                    return double.NaN;
                case ESType.Null:
                    return 0;
                case ESType.Boolean:
                    if ((bool)input.Value) return 1;
                    return 0;
                case ESType.String:
                    double res;
                    double.TryParse((string)input.Value, out res);
                    return res;
                case ESType.Number:
                    return (double)input.Value;
                case ESType.Object:
                    return ToNumber(ToPrimitive(input, ESType.Number));

                default:
                    break;
            }

            return 0;
        }

        internal static double ToInteger(Primitive input)
        {
            double d = ToNumber(input);
            switch (d)
            {
                case 0:
                case -double.Epsilon:
                case double.NegativeInfinity:
                case double.PositiveInfinity:
                    return d;
                case double.NaN:
                    return 0;
                default:
                    return Math.Sign(d*Math.Floor(Math.Abs(d)));
            }
        }

        internal static double ToInt32(Primitive input)
        {
            double d = ToNumber(input);
            switch (d)
            {
                case 0:
                case -double.Epsilon:
                case double.NegativeInfinity:
                case double.PositiveInfinity:
                case double.NaN:
                    return 0;
                default:
                    double res = Math.IEEERemainder(Math.Sign(d * Math.Floor(Math.Abs(d))), 4294967296.0);
                    if (res >= 2147483648.0) return res - 4294967296.0;
                    return res;
            }
        }

        internal static double ToUint32(Primitive input)
        {
            double d = ToNumber(input);
            switch (d)
            {
                case 0:
                case -double.Epsilon:
                case double.NegativeInfinity:
                case double.PositiveInfinity:
                case double.NaN:
                    return 0;
                default:
                    return Math.IEEERemainder(Math.Sign(d * Math.Floor(Math.Abs(d))), 4294967296.0);
            }
        }

        internal static double ToUint16(Primitive input)
        {
            double d = ToNumber(input);
            switch (d)
            {
                case 0:
                case -double.Epsilon:
                case double.NegativeInfinity:
                case double.PositiveInfinity:
                case double.NaN:
                    return 0;
                default:
                    return Math.IEEERemainder(Math.Sign(d * Math.Floor(Math.Abs(d))), 65536.0);
            }
        }

        internal static string ToString(Primitive input)
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

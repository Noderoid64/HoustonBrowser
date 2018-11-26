using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HoustonBrowser.JS
{
    enum ESType
    {
        Undefined, Null, Boolean, String, Number, Object, Reference, List, Completion, NaN, NegativeInfinity, PositiveInfinity, NegativeZero
    }

    [Flags]
    enum Attributes
    {
        ReadOnly=1, DontEnum=2, DontDelete=4, Internal=8
    }

    class Property
    {
        private Attributes attrs;
        private Primitive value;

        public Property()
        {
        }

        public Property(Attributes attrs, Primitive value)
        {
            this.attrs = attrs;
            this.value = value;
        }

        internal Attributes Attrs { get => attrs; set => attrs = value; }
        internal Primitive Value { get => value; set => this.value = value; }
    }

    class Primitive
    {
        private ESType type;
        private object value;

        public Primitive()
        {
        }

        public Primitive(ESType type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public object Value { get => value; set => this.value = value; }
        public ESType Type { get => type; set => type = value; }
    }

    class VarObject : Primitive
    {
        private VarObject prototype;
        private string @class;
        private VarObject scope;
        private Func<List<Primitive>, Primitive> code;
        private Dictionary<string, Property> properties = new Dictionary<string, Property>();

        public Func<List<Primitive>, Primitive> Code { get => code; set => code = value; }
        public VarObject Scope { get => scope; set => scope = value; }

        public VarObject(VarObject proto, string @class, Func<List<Primitive>, Primitive> code = null)
        {
            this.prototype = proto;
            this.@class = @class;
            this.code = code;
        }

        public Primitive Get(string p)
        {
            VarObject hostObject = this;
            while (hostObject != null)
            {
                if (properties.ContainsKey(p)) return properties[p].Value;
                hostObject = hostObject.prototype;
            }
            Primitive prop = new Primitive(ESType.Undefined, null);
            properties.Add(p, new Property(0, prop));
            return prop;
        }

        public void Put(string p, Primitive v, Attributes attributes = 0)
        {
            if (CanPut(p))
            {
                if (properties.ContainsKey(p)) properties[p].Value = v;
                else properties.Add(p, new Property(attributes, v));
            }
        }

        public bool CanPut(string p)
        {
            VarObject hostObject = this;
            while (true)
            {
                if (properties.ContainsKey(p))
                {
                    Property tuple = hostObject.properties[p];
                    if ((tuple.Attrs & Attributes.ReadOnly) == Attributes.ReadOnly) return false;
                    return true;
                }
                if (hostObject.prototype == null) return true;
                hostObject = hostObject.prototype;
            }
        }

        public bool HasProperty(string p)
        {
            VarObject hostObject = this;
            while (hostObject != null)
            {
                if (hostObject.properties.ContainsKey(p)) return true;
                hostObject = hostObject.prototype;
            }
            return false;
        }

        public bool Delete(string p)
        {
            if (!properties.ContainsKey(p)) return true;
            Property tuple = properties[p];
            if ((tuple.Attrs & Attributes.DontDelete) == Attributes.DontDelete) return false;
            return properties.Remove(p); // = return true
        }

        public void DefaultValue()// TODO
        { }

        public VarObject Call(Primitive arguments)// not by spec
        {
            return Code.Invoke(arguments.Value as List<Primitive>) as VarObject;
        }

        public VarObject Construct(Primitive arguments)
        {
            return Code.Invoke(arguments.Value as List<Primitive>) as VarObject;
        }
    }

    class ESInterpreter
    {
        private UnaryExpression root;
        private Stack<VarObject> scopeChain = new Stack<VarObject>();

        public ESInterpreter(VarObject globalObject)
        {
            scopeChain.Push(globalObject);
        }

        public void Process(UnaryExpression rootExpr)
        {
            Stack<UnaryExpression> stack = new Stack<UnaryExpression>();

            root = rootExpr;
            stack.Push(rootExpr);

            while (stack.Count != 0)
            {
                UnaryExpression node = stack.Pop();
                    switch (node.Type)
                    {
                        case ExpressionType.Block:
                            Block block = node as Block;
                            for (int i = block.Body.Count-1; i >=0; i--)
                            {
                                stack.Push(block.Body[i]);
                            }
                            break;

                        case ExpressionType.VariableDeclaration:
                            VariableDeclaration variable = node as VariableDeclaration;
                            foreach (var item in variable.Declarations)
                            {
                                scopeChain.Peek().Put(item.Id, EvalExpression(item.FirstValue));
                            }
                            break;

                        case ExpressionType.AssignmentExpression:
                            BinaryExpression binaryExpression = node as BinaryExpression;
                            Primitive a = EvalExpression(binaryExpression.FirstValue);
                            Primitive b = EvalExpression(binaryExpression.SecondValue);
                            a.Type = b.Type;
                            a.Value = b.Value;
                            break;

                        case ExpressionType.CallExpression:
                            BinaryExpression callexpr = node as BinaryExpression;
                            VarObject memb = EvalExpression(callexpr.FirstValue) as VarObject;
                            Primitive args = EvalExpression(callexpr.SecondValue) as Primitive;
                            memb.Call(args);
                            break;
                        
                    case ExpressionType.IfExpression:
                        IfExpression ifExpr = node as IfExpression;
                        if (TypeConverter.ToBoolean(EvalExpression(ifExpr.Cond)))
                        {
                            stack.Push(ifExpr.FirstValue);
                        }
                        else
                        {
                            if(ifExpr.SecondValue!=null) stack.Push(ifExpr.SecondValue);
                        }
                        break;

                    default:
                            break;
                    }
            }
        }

        public Primitive EvalExpression(UnaryExpression expression)
        {
            switch (expression.Type)
            {
                case ExpressionType.Undefined:
                    return new Primitive(ESType.Undefined, null);
                case ExpressionType.Null:
                    return new Primitive(ESType.Null, null);
                case ExpressionType.Boolean:
                    SimpleExpression boolean = expression as SimpleExpression;
                    return new Primitive(ESType.Boolean, Convert.ToBoolean(boolean.Value));
                case ExpressionType.String:
                    SimpleExpression srt = expression as SimpleExpression;
                    return new Primitive(ESType.String, srt.Value);
                case ExpressionType.Number:
                    SimpleExpression num = expression as SimpleExpression;
                    double d;
                    double.TryParse((string)num.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
                    return new Primitive(ESType.Number, d);

                case ExpressionType.Object: // TODO: add properties to this object 
                    VarObject p = new VarObject(null, "Object");

                    return p;
                case ExpressionType.Ident: // not by spec. should throw reference error
                    SimpleExpression ident = expression as SimpleExpression;
                    VarObject scope = scopeChain.Peek();
                    while (scope != null)
                    {
                        if (scope.HasProperty((string)ident.Value)) return scope.Get((string)ident.Value);
                        scope = scope.Scope;
                    }
                    return new Primitive(ESType.Undefined, null);

                case ExpressionType.MemberExpression: // not by spec. see page 52 of es262.
                    BinaryExpression memexpr = expression as BinaryExpression;
                    VarObject mObj = EvalExpression(memexpr.FirstValue) as VarObject;
                    SimpleExpression mProp = memexpr.SecondValue as SimpleExpression;
                    return mObj.Get((string)mProp.Value);

                case ExpressionType.BinaryExpression: // not by spec. see page 58 of es262.
                    BinaryExpression binary = expression as BinaryExpression;
                    Primitive a = EvalExpression(binary.FirstValue), b = EvalExpression(binary.SecondValue);
                    switch (binary.Oper)
                    {
                        case "+":
                            if (a.Type == ESType.Number && b.Type == ESType.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(ESType.Number, anum + bnum);
                            }
                            break;
                        case "-":
                            if (a.Type == ESType.Number && b.Type == ESType.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(ESType.Number, anum - bnum);
                            }
                            break;
                        case "*":
                            if (a.Type == ESType.Number && b.Type == ESType.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(ESType.Number, anum * bnum);
                            }
                            break;
                        case "/":
                            if (a.Type == ESType.Number && b.Type == ESType.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(ESType.Number, anum / bnum);
                            }
                            break;
                    }

                    break;

                case ExpressionType.Arguments:
                    Arguments primitive = expression as Arguments;
                    List<Primitive> list = new List<Primitive>();
                    if (primitive.Args != null) foreach (var item in primitive.Args)
                        {
                            list.Add(EvalExpression(item));
                        }
                    return new Primitive(ESType.List, list);

                case ExpressionType.LogicalExpression:
                    break;


                case ExpressionType.AssignmentExpression:
                    BinaryExpression binaryExpression = expression as BinaryExpression;

                    break;

                default:
                    break;
            }
            return null;
        }

        public void AddHostObject(string propertyName, VarObject hostObject)
        {
            scopeChain.Peek().Put(propertyName, hostObject);
        }
    }

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

        public static string ToString(Primitive input)
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
                    break;
                case ESType.Object:
                    break;
                default:
                    break;
            }

            return "unkown";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HoustonBrowser.JS
{
    enum Types
    {
        Undefined,Null,Boolean,String,Number,Object,Reference,List,Completion
    }

    [Flags]
    enum Attributes
    {
        ReadOnly=1, DontEnum=2, DontDelete=4, Internal=8
    }

    class Property
    {
        Attributes attrs;
        Primitive value;

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
        Types type;
        object value;
        Func<List<Primitive>, Primitive> code;

        public Primitive()
        {
        }

        public Primitive(Types type, object value, Func<List<Primitive>, Primitive> code=null)
        {
            this.type = type;
            this.value = value;
            this.code = code;
        }

        public object Value { get => value; set => this.value = value; }
        internal Types Type { get => type; set => type = value; }
        internal Func<List<Primitive>, Primitive> Code { get => code; set => code = value; }
    }

    class VarObject : Primitive
    {
        VarObject prototype;
        string clas;
        VarObject scope;

        Dictionary<string, Property> properties = new Dictionary<string, Property>();

        public VarObject Scope { get => scope; set => scope = value; }

        public VarObject(VarObject proto, string clas, Func<List<Primitive>, Primitive> code = null)
        {
            this.prototype = proto;
            this.clas = clas;
            this.Code = code;
        }

        public Primitive Get(string p)
        {
            VarObject hostObject = this;
            while (hostObject != null)
            {
                if (properties.ContainsKey(p)) return properties[p].Value;
                hostObject = hostObject.prototype;
            }
            Primitive prop = new Primitive(Types.Undefined, null);
            properties.Add(p,new Property(0,prop));
            return prop;
        }

        public void Put(string p, Primitive v)
        {
            if (CanPut(p))
            {
                if (properties.ContainsKey(p)) properties[p].Value = v;
                else properties.Add(p, new Property(0,v));
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
                hostObject=hostObject.prototype;
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
        public virtual void Call(Primitive arguments)// not by spec
        {
            List<Primitive> args = arguments == null ? new List<Primitive>(): arguments.Value as List<Primitive>;
            Code.Invoke(args);
        }
    }

    class HostObject : VarObject
    {
        public HostObject(VarObject proto, string clas, Func<List<Primitive>, Primitive> code = null) : base(proto, clas,code)
        {
        }
    }

    class ESInterpreter
    {
        UnaryExpression root;

        Stack<VarObject> scopeChain = new Stack<VarObject>();

        public ESInterpreter()
        {
            scopeChain.Push(new HostObject(null,"Global Object"));
            HostObject go = scopeChain.Peek() as HostObject;
            HostObject console = new HostObject(null, "Object");
            console.Put("log",new HostObject(null, "Function", x => {
                StringBuilder sb = new StringBuilder();
                foreach (var item in x)
                {
                    switch (item.Type)
                    {
                        case Types.Undefined:
                            sb.Append("undefined");
                            break;
                        case Types.Null:
                            sb.Append("null");
                            break;
                        case Types.Boolean:
                        case Types.String:
                        case Types.Number:
                        case Types.Object:
                            sb.Append(item.Value.ToString());
                            break;
                    }
                    
                }

                Console.WriteLine(sb.ToString());
                return new Primitive(Types.Undefined, null);
            }));
            go.Put("console", console);
        }

        public void Process(UnaryExpression rootExpr)
        {
            Queue<UnaryExpression> stack = new Queue<UnaryExpression>();
            HashSet<UnaryExpression> visited = new HashSet<UnaryExpression>();

            root = rootExpr;
            stack.Enqueue(rootExpr);

            while (stack.Count != 0)
            {
                UnaryExpression node = stack.Dequeue();
                if (!visited.Contains(node))
                {
                    switch (node.Type)
                    {
                        case ExpressionType.Block:
                            Block block = node as Block;
                            foreach (var item in block.Body)
                            {
                                stack.Enqueue(item);
                            }
                            break;

                        case ExpressionType.VariableDeclaration:
                            VariableDeclaration variable = node as VariableDeclaration;
                            foreach (var item in variable.Declarations)
                            {
                                scopeChain.Peek().Put(item.Id, EvalExpression(item.Init));
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
                            VarObject memb  = EvalExpression(callexpr.FirstValue) as VarObject;
                            Primitive args = EvalExpression(callexpr.SecondValue);
                            memb.Call(args);

                            break;

                        default:
                            break;
                    }
                    visited.Add(node);
                }
            }
        }

        public Primitive EvalExpression(UnaryExpression expression)
        {
            switch (expression.Type)
            {
                case ExpressionType.Undefined:
                    return new Primitive(Types.Undefined, null);
                case ExpressionType.Null:
                    return new Primitive(Types.Null, null);
                case ExpressionType.Boolean:
                    SimpleExpression boolean = expression as SimpleExpression;
                    return new Primitive(Types.Boolean,Convert.ToBoolean(boolean.Value));
                case ExpressionType.String:
                    SimpleExpression srt = expression as SimpleExpression;
                    return new Primitive(Types.String, srt.Value);
                case ExpressionType.Number:
                    SimpleExpression num = expression as SimpleExpression;
                    double d;
                    double.TryParse((string)num.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
                    return new Primitive(Types.Number, d);

                case ExpressionType.Object: // TODO: add properties to this object 
                    VarObject p = new VarObject(null,"Object");

                    return p;
                case ExpressionType.Ident: // not by spec. should throw reference error
                    SimpleExpression ident = expression as SimpleExpression;
                    VarObject scope = scopeChain.Peek();
                    while (scope!=null)
                    {
                        if (scope.HasProperty((string)ident.Value)) return scope.Get((string)ident.Value);
                        scope = scope.Scope;
                    }
                    return new Primitive(Types.Undefined, null);

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
                            if (a.Type == Types.Number && b.Type == Types.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(Types.Number, anum + bnum);
                            }
                            break;
                        case "-":
                            if (a.Type == Types.Number && b.Type == Types.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(Types.Number, anum - bnum);
                            }
                            break;
                        case "*":
                            if (a.Type == Types.Number && b.Type == Types.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(Types.Number, anum * bnum);
                            }
                            break;
                        case "/":
                            if (a.Type == Types.Number && b.Type == Types.Number)
                            {
                                double anum = (double)a.Value;
                                double bnum = (double)b.Value;
                                return new Primitive(Types.Number, anum / bnum);
                            }
                            break;
                    }

                    break;

                case ExpressionType.Arguments:
                    SimpleExpression primitive = expression as SimpleExpression;
                    List<Primitive> list = new List<Primitive>();
                    foreach (var item in primitive.Value as List<UnaryExpression>)
                    {
                        list.Add(EvalExpression(item));
                    }
                    return new Primitive(Types.List, list);

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
    }

    class TypeConvertor
    {
        public Primitive ToPrimitive(Primitive value, Types preferredType = Types.Undefined )
        {

            return value;
        }

        public Primitive ToBoolean(Primitive value)
        {
            return null;
        }

    }
}

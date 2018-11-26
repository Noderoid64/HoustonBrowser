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

    class ESInterpreter
    {
        private Stack<HostObject> scopeChain = new Stack<HostObject>();

        public ESInterpreter(HostObject globalObject)
        {
            scopeChain.Push(globalObject);
        }

        public void Process(UnaryExpression rootExpr)
        {
            Stack<UnaryExpression> expressionStack = new Stack<UnaryExpression>();

            expressionStack.Push(rootExpr);

            while (expressionStack.Count != 0)
            {
                UnaryExpression node = expressionStack.Pop();
                    switch (node.Type)
                    {
                        case ExpressionType.Block:
                            Block block = node as Block;
                            for (int i = block.Body.Count-1; i >=0; i--)
                            {
                                expressionStack.Push(block.Body[i]);
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
                            HostObject memb = EvalExpression(callexpr.FirstValue) as HostObject;
                            Primitive args = EvalExpression(callexpr.SecondValue) as Primitive;
                            memb.Call(scopeChain.Peek(), args);
                            break;
                        
                    case ExpressionType.IfExpression:
                        IfExpression ifExpr = node as IfExpression;
                        if (TypeConverter.ToBoolean(EvalExpression(ifExpr.Cond)))
                        {
                            expressionStack.Push(ifExpr.FirstValue);
                        }
                        else
                        {
                            if(ifExpr.SecondValue!=null) expressionStack.Push(ifExpr.SecondValue);
                        }
                        break;

                    case ExpressionType.Function:
                        Function func = node as Function;
                        HostObject go = scopeChain.Peek();
                        go.Put(func.Id, new NativeObject(go.Get("Object") as HostObject, "Object", func.FirstValue));
                        break;

                    case ExpressionType.NewExpression:
                        
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
                    HostObject p = new HostObject(null, "Object");

                    return p;
                case ExpressionType.Ident: // not by spec. should throw reference error
                    SimpleExpression ident = expression as SimpleExpression;
                    HostObject scope = scopeChain.Peek();
                    while (scope != null)
                    {
                        if (scope.HasProperty((string)ident.Value)) return scope.Get((string)ident.Value);
                        scope = scope.Scope;
                    }
                    return new Primitive(ESType.Undefined, null);

                case ExpressionType.MemberExpression: // not by spec. see page 52
                    BinaryExpression memexpr = expression as BinaryExpression;
                    HostObject mObj = EvalExpression(memexpr.FirstValue) as HostObject;
                    SimpleExpression mProp = memexpr.SecondValue as SimpleExpression;
                    if (mProp == null) return mObj;
                    return mObj.Get((string)mProp.Value);

                case ExpressionType.BinaryExpression: // not by spec. see page 58
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
                    List<Primitive> list;
                    if (primitive.Args != null)
                    {
                        list = new List<Primitive>();
                        foreach (var item in primitive.Args)
                        {
                            list.Add(EvalExpression(item));
                        }
                        return new Primitive(ESType.List, list);
                    }
                    return new Primitive(ESType.List, null);

                case ExpressionType.LogicalExpression:
                    break;

                case ExpressionType.NewExpression:
                    BinaryExpression binaryExpression = expression as BinaryExpression;
                    HostObject first = EvalExpression(binaryExpression.FirstValue) as HostObject;
                    Primitive second = EvalExpression(binaryExpression.SecondValue);
                    return first.Call(null, second);
                    break;

                case ExpressionType.AssignmentExpression:
                    break;

                default:
                    break;
            }
            return null;
        }

        public void AddHostObject(string propertyName, HostObject hostObject)
        {
            scopeChain.Peek().Put(propertyName, hostObject);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HoustonBrowser.JS
{
    enum ESType
    {
        Undefined, Null, Boolean, String, Number, Object, Reference, List, Completion
    }

    [Flags]
    enum Attributes
    {
        ReadOnly=1, DontEnum=2, DontDelete=4, Internal=8
    }

    class ESInterpreter
    {
        private ESContext currentContext;

        internal ESContext CurrentContext { get => currentContext; set => currentContext = value; }

        internal void Process(UnaryExpression rootExpr)
        {
            currentContext.ExpressionStack.Push(rootExpr);

            while (currentContext.ExpressionStack.Count != 0)
            {
                UnaryExpression node = currentContext.ExpressionStack.Pop();
                EvalExpression(node);
            }
        }

        internal Primitive EvalExpression(UnaryExpression expression)
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

                case ExpressionType.Block:
                    ProcessBlock(expression);
                    break;

                case ExpressionType.VariableDeclaration:
                    ProcessVariableDeclaration(expression);
                    break;

                case ExpressionType.AssignmentExpression:
                    ProcessAssignmentExpression(expression);
                    break;

                case ExpressionType.IfExpression:
                    ProcessIfExpression(expression);
                    break;

                case ExpressionType.FunctionDeclaration:
                    ProcessFunctionDeclaration(expression);
                    break;
                case ExpressionType.Object: // TODO: add properties to this object 
                    HostObject p = new HostObject(null, "Object");
                    return p;

                case ExpressionType.Ident: // not by spec. should throw reference error
                    return ProcessIdent(expression);

                case ExpressionType.MemberExpression: // not by spec. see page 52
                    return ProcessMemberExpression(expression);

                case ExpressionType.BinaryExpression: // not by spec. see page 58
                    BinaryExpression binary = expression as BinaryExpression;
                    Primitive leftOp = EvalExpression(binary.FirstValue), rigthOp = EvalExpression(binary.SecondValue);
                    switch (binary.Oper)
                    {
                        case "+":
                            if (leftOp.Type == ESType.Number && rigthOp.Type == ESType.Number)
                            {
                                double anum = (double)leftOp.Value;
                                double bnum = (double)rigthOp.Value;
                                return new Primitive(ESType.Number, anum + bnum);
                            }
                            break;
                        case "-":
                            if (leftOp.Type == ESType.Number && rigthOp.Type == ESType.Number)
                            {
                                double anum = (double)leftOp.Value;
                                double bnum = (double)rigthOp.Value;
                                return new Primitive(ESType.Number, anum - bnum);
                            }
                            break;
                        case "*":
                            if (leftOp.Type == ESType.Number && rigthOp.Type == ESType.Number)
                            {
                                double anum = (double)leftOp.Value;
                                double bnum = (double)rigthOp.Value;
                                return new Primitive(ESType.Number, anum * bnum);
                            }
                            break;
                        case "/":
                            if (leftOp.Type == ESType.Number && rigthOp.Type == ESType.Number)
                            {
                                double anum = (double)leftOp.Value;
                                double bnum = (double)rigthOp.Value;
                                return new Primitive(ESType.Number, anum / bnum);
                            }
                            break;
                    }
                    break;

                case ExpressionType.Arguments:
                    return ProcessArguments(expression);

                case ExpressionType.CallExpression:
                    return ProcessCallExpression(expression);

                case ExpressionType.NewExpression:
                    return ProcessNewExpression(expression);

                default:
                    break;
            }
            return null;
        }

        private Primitive ProcessNewExpression(UnaryExpression expression)
        {
            BinaryExpression binaryExpression = expression as BinaryExpression;
            HostObject first = EvalExpression(binaryExpression.FirstValue) as HostObject;
            Primitive second = EvalExpression(binaryExpression.SecondValue);
            return first.Construct(null, second);
        }

        private Primitive ProcessCallExpression(UnaryExpression expression)
        {
            BinaryExpression callexpr = expression as BinaryExpression;
            HostObject @this = currentContext.This;
            if (callexpr.FirstValue.FirstValue != null) @this = EvalExpression(callexpr.FirstValue.FirstValue) as HostObject;
            HostObject memb = EvalExpression(callexpr.FirstValue) as HostObject;
            Primitive args = EvalExpression(callexpr.SecondValue) as Primitive;
            return memb.Call(@this, args);
        }

        private Primitive ProcessArguments(UnaryExpression expression)
        {
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
        }

        private Primitive ProcessMemberExpression(UnaryExpression expression)
        {
            BinaryExpression memexpr = expression as BinaryExpression;
            HostObject mObj = EvalExpression(memexpr.FirstValue) as HostObject;
            SimpleExpression mProp = memexpr.SecondValue as SimpleExpression;
            if (mProp == null) return mObj;
            return mObj.Get((string)mProp.Value);
        }

        private Primitive ProcessIdent(UnaryExpression expression)
        {
            SimpleExpression ident = expression as SimpleExpression;
            HostObject scope = currentContext.This;
            return scope.Get((string)ident.Value);
        }

        internal void ProcessFunctionDeclaration(UnaryExpression expression)
        {
            FunctionDeclaration func = expression as FunctionDeclaration;
            NativeObject funcObj = CreateFunction(expression);
            currentContext.This.Put(func.Id, funcObj);
        }

        private void ProcessIfExpression(UnaryExpression expression)
        {
            IfExpression ifExpr = expression as IfExpression;
            if (TypeConverter.ToBoolean(EvalExpression(ifExpr.Cond)))
            {
                currentContext.ExpressionStack.Push(ifExpr.FirstValue);
            }
            else
            {
                if (ifExpr.SecondValue != null) currentContext.ExpressionStack.Push(ifExpr.SecondValue);
            }
        }

        private void ProcessAssignmentExpression(UnaryExpression expression)
        {
            BinaryExpression assignmentExpr = expression as BinaryExpression;
            Primitive a = EvalExpression(assignmentExpr.FirstValue);
            Primitive b = EvalExpression(assignmentExpr.SecondValue);
            if (b.Type == ESType.Object)
            {

            }
            else
            {
                a.Type = b.Type;
                a.Value = b.Value;
            }
            a = b;
        }

        private void ProcessVariableDeclaration(UnaryExpression expression)
        {
            VariableDeclaration variable = expression as VariableDeclaration;
            foreach (var item in variable.Declarations)
            {
                currentContext.This.Put(item.Id, EvalExpression(item.FirstValue));
            }
        }

        private void ProcessBlock(UnaryExpression expression)
        {
            Block block = expression as Block;
            for (int i = block.Body.Count - 1; i >= 0; i--)
            {
                currentContext.ExpressionStack.Push(block.Body[i]);
            }
        }

        private NativeObject CreateFunction(UnaryExpression expression)
        {
            FunctionDeclaration func = expression as FunctionDeclaration;
            HostObject go = currentContext.GlobalObject;
            NativeObject newFunc = new NativeObject((go.Get("Function") as HostObject).Prototype, "Function", func.FirstValue);
            NativeObject newObj = new NativeObject((go.Get("Object") as HostObject).Prototype, "Object");
            newObj.Put("constructor", newFunc, Attributes.DontEnum);
            newFunc.Scope = go;
            newFunc.Put("length", new Primitive(ESType.Number, func.Parameters == null ? 0 : func.Parameters.Count));
            newFunc.Put("prototype", newObj, Attributes.DontDelete);
            return newFunc;
        }
    }
}

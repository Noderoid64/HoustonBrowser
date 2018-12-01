using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.JS
{
    public class JSModule : IJS
    {
        private ESTokenizer tokenizer = new ESTokenizer();
        private ESParser parser = new ESParser();
        private ESInterpreter interpreter;

        public event EventHandler<string> onAlert;

        public JSModule()
        {
            interpreter = new ESInterpreter();
            ESContext context = new ESContext(CreateWindowObject());
            context.AddHostObject("console", CreateConsoleObject());
            interpreter.CurrentContext = context;
        }

        public string Process(string rawJS)
        {
            List<Token> list = tokenizer.Tokenize(rawJS);
            if (list == null) throw new Exception("Error occured during js tokenisation");
            parser.Init(list);
            UnaryExpression root = parser.Program();
            if (root == null) throw new Exception("Error occured during js parsing");
            interpreter.Process(root);
            return "OK";
        }

        private HostObject CreateWindowObject()
        {
            HostObject window = new HostObject(null, "Window");
            HostObject @object = CreateObjectObject();
            window.Put("Object", @object);
            window.Put("Function", CreateFunctionObject(@object));
            HostObject alert = new HostObject(@object, "Object", (caller,x) => {
                StringBuilder sb = new StringBuilder();
                if (x != null) foreach (var item in x)
                    {
                        sb.Append(TypeConverter.ToString(item));
                    }
                onAlert(this, sb.ToString());
                return new Primitive(ESType.Undefined, null);
            });
            window.Put("alert", alert);
            return window;
        }

        private HostObject CreateObjectObject()
        {
            HostObject objectProto = new HostObject(null, "Object");
            objectProto.Put("toString", new HostObject(null, "Function", (caller, x) => {
                return new Primitive(ESType.String, $"[object {caller.Class}]");
            }));
            objectProto.Put("valueOf", new HostObject(null, "Function", (caller, x) => {
                return caller;
            }));
            objectProto.Put("hasOwnProperty", new HostObject(null, "Function", (caller, x) => {
                string prop = "undefined";
                if (x != null) prop = TypeConverter.ToString(x[0]);
                if (caller.Properties.ContainsKey(prop)) return new Primitive(ESType.Boolean, true);
                return new Primitive(ESType.Boolean, false);
            }));
            objectProto.Put("isPrototypeOf", new HostObject(null, "Function", (caller, x) => {
                if (x == null || x[0].Type != ESType.Object) return new Primitive(ESType.Boolean, false);
                HostObject a = x[0] as HostObject;
                if (a.Prototype == null || a.Prototype!=caller.Prototype)return new Primitive(ESType.Boolean, false);
                return new Primitive(ESType.Boolean, true);
            }));
            objectProto.Put("propertyIsEnumerable", new HostObject(null, "Function", (caller, x) => {
                string prop = "undefined";
                if (x != null) prop = TypeConverter.ToString(x[0]);
                if (caller.Properties.ContainsKey(prop) && (caller.Properties[prop].Attrs & Attributes.DontEnum)== Attributes.DontEnum) return new Primitive(ESType.Boolean, true);
                return new Primitive(ESType.Boolean, false);
            }));
            HostObject @object = new HostObject(objectProto, "Object",null);
            objectProto.Put("constructor", @object);
            @object.ConstructMethod = (caller, x) =>
            {
                if (x == null || x[0].Type == ESType.Undefined || x[0].Type == ESType.Null)
                {
                    NativeObject obj = new NativeObject(@object, "Object");
                    return obj;
                }
                else
                {
                    //return new  NUmber string or boolean objects
                }
                return new Primitive(ESType.Undefined, null);
            };
            @object.Put("prototype", objectProto);

            return @object;
        }
        
        private HostObject CreateFunctionObject(HostObject @object)
        {
            HostObject funcProto = new HostObject(@object, "Function", (caller, x) => new Primitive(ESType.Undefined, null));
            HostObject funcObj = new HostObject(funcProto, "Function",null,(caller,x)=> {
                StringBuilder parameters = new StringBuilder();
                string body="";
                if (x != null)
                {
                    for (int i = 0; i < x.Count-1; i++)
                    {
                        parameters.Append(TypeConverter.ToString(x[i]));
                        if (i != x.Count - 2) parameters.Append(",");
                    }
                    body = TypeConverter.ToString(x[x.Count - 1]);
                }
                List<Token> list = tokenizer.Tokenize(parameters.ToString());
                if (list == null) ; // throw SyntaxError
                parser.Init(list);
                List<SimpleExpression> paramsList = parser.FormalParameterList();
                list = tokenizer.Tokenize(body.ToString());
                if (list == null) ; // throw SyntaxError
                parser.Init(list);
                UnaryExpression bodyExpr = parser.FunctionBody();
                return interpreter.EvalExpression(new FunctionDeclaration("", paramsList, bodyExpr));
            });
            funcProto.Put("constructor", funcObj);

            return funcObj;
        }


        private HostObject CreateConsoleObject()
        {
            HostObject console = new HostObject(null, "Object");
            HostObject log = new HostObject(null, "Object", (caller,x) => {
                StringBuilder sb = new StringBuilder();
                if(x!=null)foreach (var item in x)
                {                        
                    sb.Append(TypeConverter.ToString(item));
                }

                Console.WriteLine(sb.ToString());
                return new Primitive(ESType.Undefined, null);
            });
            console.Put("log", log);

            return console;
        }
    }
}

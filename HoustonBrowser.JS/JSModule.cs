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
            interpreter = new ESInterpreter(CreateWindowObject());
            CreateConsoleObject(interpreter);
        }

        public string Process(string rawJS)
        {
            List<Token> list = tokenizer.Tokenize(rawJS);
            if (list == null) throw new Exception("Error occured during js tokenisation");
            UnaryExpression root = parser.Program(list);
            if (root == null) throw new Exception("Error occured during js parsing");
            interpreter.Process(root);
            return "OK";
        }

        private VarObject CreateWindowObject()
        {
            VarObject window = new VarObject(null, "Window");
            VarObject alert = new VarObject(null, "Object", x => {
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

        private void CreateConsoleObject(ESInterpreter interpreter)
        {
            VarObject console = new VarObject(null, "Object");
            VarObject log = new VarObject(null, "Object", x => {
                StringBuilder sb = new StringBuilder();
                foreach (var item in x)
                {
                    switch (item.Type)
                    {
                        case ESType.Undefined:
                            sb.Append("undefined");
                            break;
                        case ESType.Null:
                            sb.Append("null");
                            break;
                        case ESType.Boolean:
                        case ESType.String:
                        case ESType.Number:
                        case ESType.Object:
                            sb.Append(item.Value.ToString());
                            break;
                    }

                }

                Console.WriteLine(sb.ToString());
                return new Primitive(ESType.Undefined, null);
            });
            console.Put("log", log);

            interpreter.AddHostObject("console", console);
        }

    }
}

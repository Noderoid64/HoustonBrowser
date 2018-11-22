using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.JS
{
    public class JSModule : IJS
    {
        private ESTokenizer tokenizer = new ESTokenizer();
        private ESParser parser = new ESParser();
        private ESInterpreter interpreter = new ESInterpreter();

        public event EventHandler<string> onAlert;

        public string Process(string rawJS)
        {
            List<Token> list = tokenizer.Tokenize(rawJS);
            if (list == null) throw new Exception("Error occured during js tokenisation");
            UnaryExpression root = parser.Program(list);
            if (root == null) throw new Exception("Error occured during js parsing");
            interpreter.Process(root);
            return "OK";
        }
    }
}

using HoustonBrowser.DOM;
using HoustonBrowser.DOM.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.JS
{
    class ESContext
    {
        private HostObject globalObject;
        private Stack<HostObject> execContextStack = new Stack<HostObject>();
        private Stack<UnaryExpression> expressionStack = new Stack<UnaryExpression>();
        private Document document;

        public ESContext(HostObject globalObject)
        {
            this.globalObject = globalObject;
            execContextStack.Push(globalObject);
        }

        public ESContext(HostObject globalObject, Document document)
        {
            this.globalObject = globalObject;
            this.document = document;
            execContextStack.Push(globalObject);
        }

        public Document Document { get => document; }
        internal HostObject GlobalObject { get => globalObject;}
        internal Stack<UnaryExpression> ExpressionStack { get => expressionStack;}
        internal Stack<HostObject> ExecContextStack { get => execContextStack; }

        internal void AddHostObject(string propertyName, HostObject hostObject)
        {
            globalObject.Put(propertyName, hostObject);
        }
    }
}

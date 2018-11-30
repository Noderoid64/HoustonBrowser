using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.JS
{
    class ESContext
    {
        private readonly HostObject globalObject;
        private readonly Stack<HostObject> execContextStack = new Stack<HostObject>();
        private readonly Stack<UnaryExpression> expressionStack = new Stack<UnaryExpression>();

        public ESContext(HostObject globalObject)
        {
            this.globalObject = globalObject;
            execContextStack.Push(globalObject);
        }

        internal HostObject GlobalObject { get => globalObject;}
        internal Stack<UnaryExpression> ExpressionStack { get => expressionStack;}
        internal Stack<HostObject> ExecContextStack { get => execContextStack; }

        internal void AddHostObject(string propertyName, HostObject hostObject)
        {
            globalObject.Put(propertyName, hostObject);
        }
    }
}

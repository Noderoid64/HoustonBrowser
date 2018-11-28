using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.JS
{
    class ESContext
    {
        private HostObject globalObject;
        private HostObject @this;
        private Stack<UnaryExpression> expressionStack = new Stack<UnaryExpression>();

        public ESContext(HostObject globalObject)
        {
            this.globalObject = globalObject;
            this.@this = globalObject;
        }

        internal HostObject GlobalObject { get => globalObject;}
        internal Stack<UnaryExpression> ExpressionStack { get => expressionStack;}
        internal HostObject This { get => @this; set => @this = value; }
        
        internal void AddHostObject(string propertyName, HostObject hostObject)
        {
            globalObject.Put(propertyName, hostObject);
        }
    }
}

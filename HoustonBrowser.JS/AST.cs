using System;
using System.Collections.Generic;

namespace HoustonBrowser.JS
{
    enum ExpressionType
    {
        Undefined, Null, Boolean, String, Number, Object,
        Ident, MemberExpression, Block, VariableDeclaration,
        VariableDeclarator, BinaryExpression, LogicalExpression,
        FunctionDeclaration, AssignmentExpression, CallExpression, Arguments,
        IfExpression, NewExpression, ReturnExpression, WhileExpression
    }

    class UnaryExpression
    {
        protected ExpressionType type;
        protected UnaryExpression firstValue;
        protected string oper;

        public UnaryExpression()
        {
        }

        public UnaryExpression(ExpressionType type) : this(type, null, null)
        {
        }

        public UnaryExpression(ExpressionType type, UnaryExpression value) : this(type, value, null)
        { }

        public UnaryExpression(ExpressionType type, UnaryExpression value, string oper)
        {
            this.type = type;
            this.firstValue = value;
            this.oper = oper;
        }

        public string Oper { get => oper; set => oper = value; }
        public UnaryExpression FirstValue { get => firstValue; set => firstValue = value; }
        public ExpressionType Type { get => type; set => type = value; }
    }

    class SimpleExpression : UnaryExpression
    {
        private object value;

        public SimpleExpression(ExpressionType type, object value) : base(type)
        {
            this.value = value;
        }

        public object Value { get => value; set => this.value = value; }
    }

    class BinaryExpression : UnaryExpression
    {
        private UnaryExpression secondValue;

        public BinaryExpression()
        {
        }

        public BinaryExpression(ExpressionType type, UnaryExpression firstValue, UnaryExpression secondValue, string op) : base(type, firstValue, op)
        {
            this.secondValue = secondValue;
        }

        public UnaryExpression SecondValue { get => secondValue; set => secondValue = value; }
    }

    class VariableDeclaration : UnaryExpression
    {
        private List<VariableDeclarator> declarations = new List<VariableDeclarator>();

        public VariableDeclaration() : base(ExpressionType.VariableDeclaration)
        {
        }

        public List<VariableDeclarator> Declarations { get => declarations; set => declarations = value; }
    }

    class VariableDeclarator : UnaryExpression
    {
        private string id;

        public VariableDeclarator(string id, UnaryExpression init) : base(ExpressionType.VariableDeclarator, init)
        {
            this.id = id;
        }

        public string Id { get => id; }
    }

    class FunctionDeclaration : UnaryExpression
    {
        private string id;
        private List<SimpleExpression> parameters;

        public FunctionDeclaration(string id, List<SimpleExpression> parameters, UnaryExpression body) : base(ExpressionType.FunctionDeclaration, body)
        {
            this.id = id;
            this.parameters = parameters;
        }

        public string Id { get => id; set => id = value; }
        public List<SimpleExpression> Parameters { get => parameters; }
    }

    class Arguments : UnaryExpression
    {
        private List<UnaryExpression> args = new List<UnaryExpression>();

        public Arguments(List<UnaryExpression> args) : base(ExpressionType.Arguments)
        {
            this.args = args;
        }

        public List<UnaryExpression> Args { get => args; set => args = value; }
    }

    class Block : UnaryExpression
    {
        private List<UnaryExpression> body = new List<UnaryExpression>();

        public Block() : base(ExpressionType.Block)
        {
        }

        public Block(List<UnaryExpression> body) : base(ExpressionType.Block)
        {
            this.body = body;
        }

        public List<UnaryExpression> Body { get => body; set => body = value; }
    }

    class IfExpression : BinaryExpression
    {
        private UnaryExpression condition;

        public IfExpression() : base(ExpressionType.IfExpression,null,null,null)
        {
        }

        public IfExpression(UnaryExpression condition, UnaryExpression firstExpr, UnaryExpression secondExpr) : base(ExpressionType.IfExpression, firstExpr, secondExpr, null)
        {
            this.condition = condition;
        }

        public UnaryExpression Cond { get => condition; set => condition = value; }
    }
}

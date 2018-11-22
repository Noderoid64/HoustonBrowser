using System;
using System.Collections.Generic;

namespace HoustonBrowser.JS
{
    public enum ExpressionType {Undefined,Null,Boolean,String,Number,Object,
       Ident, MemberExpression,Block, VariableDeclaration, BinaryExpression, LogicalExpression, Function,AssignmentExpression,CallExpression,Arguments
    }

    public class UnaryExpression
    {
        protected ExpressionType type;
        protected UnaryExpression firstValue;
        string oper;

        public UnaryExpression()
        {
        }

        public UnaryExpression(ExpressionType type)
        {
            this.type = type;
        }

        public UnaryExpression(ExpressionType type, UnaryExpression value)
        {
            this.type = type;
            this.FirstValue = value;
        }

        public UnaryExpression(UnaryExpression firstValue, string oper)
        {
            this.firstValue = firstValue;
            this.oper = oper;
        }

        public string Oper { get => oper; set => oper = value; }
        public UnaryExpression FirstValue { get => firstValue; set => firstValue = value; }
        public ExpressionType Type { get => type; set => type = value; }
    }

    public class SimpleExpression : UnaryExpression
    {
        object value;

        public SimpleExpression(ExpressionType type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public object Value { get => value; set => this.value = value; }
    }

    public class BinaryExpression : UnaryExpression
    {
        UnaryExpression secondValue;

        public BinaryExpression()
        {
        }

        public BinaryExpression(UnaryExpression firstValue, UnaryExpression secondValue,string op, ExpressionType type) :base(firstValue,op)
        {
            this.type = type;
            this.secondValue = secondValue;
        }

        public UnaryExpression SecondValue { get => secondValue; set => secondValue = value; }
    }

    public class VariableDeclaration : UnaryExpression
    {
        List<VariableDeclarator> declarations = new List<VariableDeclarator>();

        public VariableDeclaration() : base(ExpressionType.VariableDeclaration)
        {
        }

        public List<VariableDeclarator> Declarations { get => declarations; set => declarations = value; }
    }

    public class VariableDeclarator : UnaryExpression
    {
        string id;
        UnaryExpression init;

        public VariableDeclarator(string id,UnaryExpression init)
        {
            this.id = id;
            this.init = init;
        }

        public string Id { get => id; }
        public UnaryExpression Init { get => init; set => init = value; }
    }

    public class Function : UnaryExpression
    {
        string id;
        List<SimpleExpression> parameters = new List<SimpleExpression>();

        public Function(string id, List<SimpleExpression> parameters, UnaryExpression body)
        {
            this.type = ExpressionType.Function;
            this.id = id;
            this.parameters = parameters;
            this.firstValue = body;
        }

        public string Id { get => id; set => id = value; }

    }

    public class Block : UnaryExpression
    {
        List<UnaryExpression> body = new  List<UnaryExpression>();

        public Block():base(ExpressionType.Block)
        {
        }

        public  List<UnaryExpression> Body { get => body; set => body = value; }
    }

}

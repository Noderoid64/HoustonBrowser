using System;
using System.Collections.Generic;

namespace HoustonBrowser.JS
{
    /* 
TODO:
0)Creating ES primitives during parsing
    1) SwitchStatement CaseBlock CaseClauses CaseClause DefaultClause
    2) FunctionExpression
    3) TryStatement, ThrowStatement, Catch, Finally
    4) LabelledStatement
    5) IterationStatement
    6) InitialiserNoIn,  VariableDeclarationNoIn, VariableDeclarationListNoIn
    ExpressionNoIn, AssignmentExpressionNoIn, ConditionalExpressionNoIn
    LogicalORExpressionNoIn, LogicalANDExpressionNoIn, BitwiseORExpressionNoIn,
    BitwiseXORExpressionNoIn, BitwiseANDExpressionNoIn, EqualityExpressionNoIn,
    RelationalExpressionNoIn, 
    7)ArrayLiteral, Elision, ElementList
    */

    class ESParser
    {
        List<Token> tokens;

        int pos;

        bool Match(string expected)
        {
            try
            {
                if (expected == tokens[pos].GetTokenValue()) { pos++; return true; }
                return false;
            }
            catch (System.IndexOutOfRangeException)
            {
                return false;
            }
        }

        public UnaryExpression Program(List<Token> w)
        {
            try
            {
                pos = 0;
                tokens = w;
                Block node = new Block();
                node.Body = SourseElements();
                return node;
            }
            catch
            {
                return null;
            }
        }

        List<UnaryExpression> SourseElements()
        {
            int oldPos = pos;
            List<UnaryExpression> nodes = new List<UnaryExpression>();
            UnaryExpression node;
            List<UnaryExpression> tempNodes;
            if ((node = SourseElement()) != null)
            {
                nodes.Add(node);
                tempNodes = SourseElements1();
                if (tempNodes != null)
                    foreach (var item in tempNodes)
                    {
                        nodes.Add(item);
                    }
                return nodes;
            }
            pos = oldPos;
            return null;
        }

        List<UnaryExpression> SourseElements1()//e
        {
            if (pos == tokens.Count) return null;
            int oldPos = pos;
            List<UnaryExpression> nodes = new List<UnaryExpression>();
            List<UnaryExpression> tempNodes;
            UnaryExpression node;
            if ((node = SourseElement()) != null)
            {
                nodes.Add(node);
                tempNodes = SourseElements1();
                if (tempNodes != null)
                    foreach (var item in tempNodes)
                    {
                        nodes.Add(item);
                    }
                return nodes;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression SourseElement()
        {
            int oldPos = pos;
            UnaryExpression node;
            switch (tokens[pos].GetTokenValue())
            {
                case "break":
                case "else":
                case "new":
                case "case":
                case "finally":
                case "return":
                case "void":
                case "catch":
                case "continue":
                case "default":
                case "throw":
                case "delete":
                case "in":
                case "instanceof":
                    throw new Exception("Unexpected token: " + tokens[pos].GetTokenValue());
            }
            if ((node = FunctionDeclaration()) != null) return node;
            pos = oldPos;
            if ((node = Statement()) != null) return node;
            pos = oldPos;
            return null;
        }

        UnaryExpression Statement()
        {
            int oldPos = pos;
            UnaryExpression node;
            // if (Block()) return true;
            // pos = oldPos;

            if ((node = VariableStatement()) != null) return node;
            pos = oldPos;

            //if (EmptyStatement()) return null;
            //pos = oldPos;

            if ((node = ExpressionStatement()) != null) return node;
            pos = oldPos;

            //if ((node = IfStatement())!=null) return node;
            //pos = oldPos;

            // IterationStatement();
            //pos = oldPos;

            // if (ContinueStatement()) return true;
            // pos = oldPos;

            // if (BreakStatement()) return true;
            // pos = oldPos;

            // if (ReturnStatement()) return true;
            // pos = oldPos;

            // if (WithStatement()) return true;
            // pos = oldPos;
            return null;
        }
        /*
                bool Block()
                {
                    int oldPos = pos;
                    if (match("{") && match("}")) return true;
                    pos = oldPos;

                    if (match("{") && StatementList() && match("}")) return true;
                    oldPos = pos;

                    return false;
                }

                bool StatementList()
                {
                    int oldPos = pos;
                    if (Statement() && StatementList1()) return true;
                    pos = oldPos;
                    return false;
                }

                bool StatementList1() //e
                {
                    int oldPos = pos;
                    if (Statement() && StatementList1()) return true;
                    pos = oldPos;
                    return true;
                }
         */
        VariableDeclaration VariableStatement()
        {
            int oldPos = pos;
            VariableDeclaration declaration = new VariableDeclaration();
            if (Match("var") && (declaration = VariableDeclarationList()) != null && Match(";")) return declaration;
            pos = oldPos;
            return null;
        }

        VariableDeclaration VariableDeclarationList()
        {
            int oldPos = pos;
            VariableDeclaration nodes = new VariableDeclaration();
            VariableDeclarator node;
            if ((node = VariableDeclaration()) != null)
            {
                nodes.Declarations = VariableDeclarationList1();
                if (nodes.Declarations == null) nodes.Declarations = new List<VariableDeclarator>();
                nodes.Declarations.Add(node);
                return nodes;
            }
            pos = oldPos;
            return null;
        }

        List<VariableDeclarator> VariableDeclarationList1()//e
        {
            int oldPos = pos;
            VariableDeclarator node;
            if (Match(",") && (node = VariableDeclaration()) != null)
            {
                List<VariableDeclarator> nodes = VariableDeclarationList1();
                if (nodes == null) nodes = new List<VariableDeclarator>();
                nodes.Add(node);

                return nodes;
            }
            pos = oldPos;
            return null;
        }

        VariableDeclarator VariableDeclaration()
        {
            int oldPos = pos;
            string ident;
            if (tokens[pos].GetTokenType() == TokenType.Identifier)
            {
                UnaryExpression init = null;
                ident = tokens[pos++].GetTokenValue();
                oldPos = pos;
                init = Initializer();
                if (init == null) pos = oldPos;
                return new VariableDeclarator(ident, init);
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression Initializer()
        {
            int oldPos = pos;
            UnaryExpression node = null;
            if (Match("=") && (node = AssignmentExpression()) != null) return node;
            pos = oldPos;
            return null;
        }

        UnaryExpression AssignmentExpression()
        {
            int oldPos = pos;
            UnaryExpression left, right;
            string oper;
            if ((left = LeftHandSideExpression()) != null &&
            (oper = AssignmentOperator()) != null &&
            (right = AssignmentExpression()) != null) return new BinaryExpression(left, right, oper, ExpressionType.AssignmentExpression);
            pos = oldPos;
            if ((left = ConditionalExpression()) != null) return left;
            pos = oldPos;
            return null;
        }

        string AssignmentOperator()
        {
            switch (tokens[pos].GetTokenValue())
            {
                case "=":
                case "*=":
                case "/=":
                case "%=":
                case "+=":
                case "-=":
                case "<<=":
                case ">>=":
                case ">>>=":
                case "&=":
                case "^=":
                case "|=":
                    //match(tokens[pos].GetTokenValue());
                    return tokens[pos++].GetTokenValue();
            }
            return null;
        }

        UnaryExpression ConditionalExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = LogicalORExpression();
            if (expr != null)
            {
                //UnaryExpression trueVariant =null, falseVariant;
                //if (match("?") &&
                //(trueVariant=AssignmentExpression())!=null &&
                //match(":") &&
                //(falseVariant=AssignmentExpression())!=null) return trueVariant;

                return expr;
            }
            pos = oldPos;
            return null;
        }

        #region Logical expressions
        UnaryExpression LogicalORExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = LogicalANDExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression next = LogicalORExpression1();
                if (next != null) return new BinaryExpression(expr, next, "||", ExpressionType.LogicalExpression);
                pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression LogicalORExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            if (Match("||") && (expr = LogicalANDExpression()) != null)
            {
                oldPos = pos;
                next = LogicalORExpression1();
                if (next != null) return new BinaryExpression(expr, next, "||", ExpressionType.LogicalExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression LogicalANDExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = BitwiseORExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression next = LogicalANDExpression1();
                if (next != null) return new BinaryExpression(expr, next, "&&", ExpressionType.LogicalExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression LogicalANDExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            if (Match("&&") &&
            (expr = BitwiseORExpression()) != null)
            {
                oldPos = pos;
                next = LogicalANDExpression1();
                if (next != null) return new BinaryExpression(expr, next, "&&", ExpressionType.LogicalExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression BitwiseORExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = BitwiseXORExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression newExpr = BitwiseORExpression1();
                if (newExpr == null) pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression BitwiseORExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            if (Match("|") &&
            (expr = BitwiseXORExpression()) != null)
            {
                oldPos = pos;
                next = BitwiseORExpression1();
                if (next != null) return new BinaryExpression(expr, next, "|", ExpressionType.BinaryExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression BitwiseXORExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = BitwiseANDExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression newExpr = BitwiseXORExpression1();
                if (newExpr == null) pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression BitwiseXORExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            if (Match("^") &&
            (expr = BitwiseANDExpression()) != null)
            {
                oldPos = pos;
                next = BitwiseXORExpression1();
                if (next != null) return new BinaryExpression(expr, next, "^", ExpressionType.BinaryExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression BitwiseANDExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = EqualityExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression newExpr = BitwiseANDExpression1();
                if (newExpr == null) pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression BitwiseANDExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            if (Match("&") &&
            (expr = EqualityExpression()) != null)
            {
                oldPos = pos;
                next = BitwiseANDExpression1();
                if (next != null) return new BinaryExpression(expr, next, "&", ExpressionType.BinaryExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }
        #endregion
        UnaryExpression EqualityExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = RelationalExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression newExpr = EqualityExpression1();
                if (newExpr == null) pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression EqualityExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            if ((Match("==") || Match("!=")) &&
            (expr = RelationalExpression()) != null)
            {
                oldPos = pos;
                next = EqualityExpression1();
                if (next != null) return new BinaryExpression(expr, next, "==", ExpressionType.BinaryExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression RelationalExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = ShiftExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression newExpr = RelationalExpression1();
                if (newExpr == null) pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression RelationalExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            switch (tokens[pos].GetTokenValue())
            {
                case ">":
                case "<":
                case ">=":
                case "<=":
                case "in":
                case "instanseof":
                    if (Match(tokens[pos].GetTokenValue()) &&
                        (expr = ShiftExpression()) != null)
                    {
                        oldPos = pos;
                        next = RelationalExpression1();
                        if (next != null) return new BinaryExpression(expr, next, ">", ExpressionType.BinaryExpression);
                        return expr;
                    }
                    break;

            }
            pos = oldPos;
            return null;
        }

        UnaryExpression ShiftExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = AdditiveExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression newExpr = ShiftExpression1();
                if (newExpr == null) pos = oldPos;
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression ShiftExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            switch (tokens[pos].GetTokenValue())
            {
                case "<<":
                case ">>":
                case ">>>":
                    if (Match(tokens[pos].GetTokenValue()) &&
                               (expr = AdditiveExpression()) != null)
                    {
                        oldPos = pos;
                        next = ShiftExpression1();
                        if (next != null) return new BinaryExpression(expr, next, ">>", ExpressionType.BinaryExpression);
                        return expr;
                    }
                    break;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression AdditiveExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = MultiplicativeExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression next = AdditiveExpression1();
                if (next != null)
                {
                    next.FirstValue = expr;
                    return next;
                }
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression AdditiveExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            switch (tokens[pos].GetTokenValue())
            {
                case "+":
                case "-":
                    string oper = tokens[pos++].GetTokenValue();
                    if ((expr = MultiplicativeExpression()) != null)
                    {
                        oldPos = pos;
                        next = AdditiveExpression1();
                        if (next != null)
                        {
                            next.FirstValue = expr;
                            return new BinaryExpression(null, next, oper, ExpressionType.BinaryExpression);
                        }
                        return new BinaryExpression(null, expr, oper, ExpressionType.BinaryExpression);
                    }
                    break;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression MultiplicativeExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = UnaryExpression();
            if (expr != null)
            {
                oldPos = pos;
                UnaryExpression next = MultiplicativeExpression1();
                if (next != null)
                {
                    next.FirstValue = expr;
                    return next;
                }
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression MultiplicativeExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            switch (tokens[pos].GetTokenValue())
            {
                case "*":
                case "/":
                case "%":
                    string oper = tokens[pos++].GetTokenValue();
                    if ((expr = UnaryExpression()) != null)
                    {
                        oldPos = pos;
                        next = MultiplicativeExpression1();
                        if (next != null)
                        {
                            next.FirstValue = expr;
                            return new BinaryExpression(null, next, oper, ExpressionType.BinaryExpression);
                        }
                        return new BinaryExpression(null, expr, oper, ExpressionType.BinaryExpression);
                    }
                    break;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression UnaryExpression()
        {
            int oldPos = pos;
            UnaryExpression expr;
            switch (tokens[pos].GetTokenValue())
            {
                case "delete":
                case "void":
                case "typeof":
                case "++":
                case "--":
                case "+":
                case "-":
                case "~":
                case "!":
                    if (Match(tokens[pos].GetTokenValue()) &&
                    (expr = UnaryExpression()) != null) return expr;
                    break;
            }
            pos = oldPos;
            if ((expr = PostfixExpression()) != null) return expr;
            return null;
        }

        UnaryExpression PostfixExpression()
        {
            int oldPos = pos;
            UnaryExpression expr = LeftHandSideExpression();
            if (expr != null)
            {
                if (Match("++") || Match("--")) pos++;
                return expr;
            }
            oldPos = pos;
            return null;
        }

        UnaryExpression LeftHandSideExpression()
        {
            UnaryExpression expr;
            int oldPos = pos;
            if ((expr = CallExpression()) != null) return expr;
            pos = oldPos;
            if ((expr = NewExpression()) != null) return expr;
            pos = oldPos;
            return null;
        }

        UnaryExpression NewExpression()
        {
            int oldPos = pos;
            UnaryExpression expr;
            //if (match("new") && NewExpression()) return true;
            //pos = oldPos;
            if ((expr = MemberExpression()) != null) return expr;
            pos = oldPos;
            return null;
        }

        UnaryExpression MemberExpression()
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            //if (match("new") && MemberExpression() &&
            //Arguments() &&
            //MemberExpression1()) return true;
            //pos = oldPos;
            if ((expr = PrimaryExpression()) != null)
            {
                next = MemberExpression1();
                if (next != null) return new BinaryExpression(expr, next, ".", ExpressionType.MemberExpression);
                return expr;
            }
            pos = oldPos;
            return null;
        }

        UnaryExpression MemberExpression1()//e
        {
            int oldPos = pos;
            UnaryExpression expr, next;
            switch (tokens[pos].GetTokenValue())
            {
                //case "[":
                //    if (match("[") && Expression() && match("]") && MemberExpression1()) return true;
                //    break;
                case ".":
                    if (Match(".") &&
                    tokens[pos].GetTokenType() == TokenType.Identifier)
                    {
                        oldPos = pos;
                        expr = new SimpleExpression(ExpressionType.Ident, tokens[pos++].GetTokenValue());
                        next = MemberExpression1();
                        if (next != null) return new BinaryExpression(expr, next, ".", ExpressionType.MemberExpression);
                        return expr;
                    }
                    break;

            }
            pos = oldPos;
            return null;
        }

        UnaryExpression PrimaryExpression()
        {
            int oldPos = pos;
            //if (match("this")) return true;
            //pos = oldPos;

            if (tokens[pos].GetTokenType() == TokenType.Identifier)
                return new SimpleExpression(ExpressionType.Ident, tokens[pos++].GetTokenValue());

            if (tokens[pos].GetTokenType() == TokenType.NullLiteral)
                return new SimpleExpression(ExpressionType.Null, tokens[pos++].GetTokenValue());

            if (tokens[pos].GetTokenType() == TokenType.NumericLiteral)
                return new SimpleExpression(ExpressionType.Number, tokens[pos++].GetTokenValue());

            if (tokens[pos].GetTokenType() == TokenType.StringLiteral)
                return new SimpleExpression(ExpressionType.String, tokens[pos++].GetTokenValue());

            UnaryExpression node;
            if ((node = ObjectLiteral()) != null) return node;

            // TODO: Array literal


            //if (match("(") && Expression() && match(")")) return true;
            //pos = oldPos;
            return null;
        }

        UnaryExpression ObjectLiteral()
        {
            int oldPos = pos;
            if (Match("{")) {
                oldPos = pos;
                //PropertyNameAndValueList(); 
                if (Match("}")) return new UnaryExpression(ExpressionType.Object, null);
            }
            pos = oldPos;
            return null;
        }

        bool PropertyNameAndValueList()
        {
            int oldPos = pos;
            //if (PropertyName() && match(":") && AssignmentExpression() && PropertyNameAndValueList1()) return true;
            pos = oldPos;
            return false;
        }

        bool PropertyNameAndValueList1()//e
        {
            int oldPos = pos;
            // if (match(",") &&
            //PropertyName() &&
            //match(":") &&
            //AssignmentExpression() &&
            //PropertyNameAndValueList1()) return true;
            pos = oldPos;
            return true;
        }

        bool PropertyName()
        {
            int oldPos = pos;
            if (tokens[pos++].GetTokenType() == TokenType.Identifier) return true;
            pos = oldPos;
            if (tokens[pos++].GetTokenType() == TokenType.StringLiteral) return true;
            pos = oldPos;
            if (tokens[pos++].GetTokenType() == TokenType.NumericLiteral) return true;
            pos = oldPos;
            return false;
        }

        UnaryExpression Expression()
        {
            int oldPos = pos;
            UnaryExpression node;
            if ((node = AssignmentExpression()) != null) {
                //Expression1();
                return node;
            }
            pos = oldPos;
            return null;
        }

        bool Expression1()//e
        {
            int oldPos = pos;
            //if (match(",") && AssignmentExpression() && Expression1()) return true;
            pos = oldPos;
            return true;
        }

        UnaryExpression Arguments()
        {
            int oldPos = pos;
            if (Match("(") && Match(")")) return new SimpleExpression(ExpressionType.Arguments,null);
            pos = oldPos;
            List<UnaryExpression> argList;
            if (Match("(") && (argList=ArgumentList())!=null && Match(")")) return new SimpleExpression(ExpressionType.Arguments, argList);
            pos = oldPos;
            return null;
        }

        List<UnaryExpression> ArgumentList()
        {
            int oldPos = pos;
            List<UnaryExpression> list = new List<UnaryExpression>(), argList;
            UnaryExpression ae;
            if ((ae=AssignmentExpression())!=null) {
                list.Add(ae);
                if ((argList = ArgumentList1()) != null){
                    foreach (var item in argList)                    
                        list.Add(item);                    
                }
                return list;

            }
            pos = oldPos;
            return null;
        }

        List<UnaryExpression> ArgumentList1()//e
        {
            int oldPos = pos;
            //if (match(",") && AssignmentExpression() && ArgumentList1()) return true;
            List<UnaryExpression> list = new List<UnaryExpression>(), argList;
            UnaryExpression ae;
            if (Match(",") && (ae = AssignmentExpression()) != null)
            {
                list.Add(ae);
                argList = ArgumentList1();
                if(argList != null)foreach (var item in argList)
                {
                    list.Add(item);
                }
                return list;
            }
            pos = oldPos;
            return null;

        }

        UnaryExpression CallExpression()
        {
            int oldPos = pos;
            UnaryExpression member = MemberExpression();
            UnaryExpression args = Arguments();
            if (member != null && args!=null)
            {
                oldPos = pos;
                //CallExpression1();
                return new BinaryExpression(member,args,null,ExpressionType.CallExpression);
            } 
            pos = oldPos;
            return null;
        }

        bool CallExpression1()//e
        {
            int oldPos = pos;
            //if (Arguments() && CallExpression1()) return true;
            pos = oldPos;
            //if (match("[") && Expression() && match("]") && CallExpression1()) return true;
            pos = oldPos;
            if (Match(".") && tokens[pos++].GetTokenType() == TokenType.Identifier && CallExpression1()) return true;
            pos = oldPos;
            return true;
        }

        bool EmptyStatement()
        {
            int oldPos = pos;
            if (Match(";")) return true;
            pos = oldPos;
            return false;
        }

        UnaryExpression ExpressionStatement()
        {
            int oldPos = pos;
            UnaryExpression node;
            if ((node=Expression())!=null && Match(";")) return node;
            pos = oldPos;
            return null;
        }
        /* 
                bool IfStatement()
                {
                    int oldPos = pos;
                    // bool firstPart = match("if") && match("(") &&
                    // Expression() && match(")") && Statement();
                    // if (firstPart)
                    // {
                    //     oldPos = pos;
                    //     if (match("else") &&
                    //     Statement()) return true;
                    //     pos = oldPos;
                    //     return true;
                    // }
                    // pos = oldPos;
                    // return false;
                    if(match("if") && match("(")) {
                        Expression =
                    Expression();
                    match(")");
                    Statement();
                    oldPos = pos;
                    if (match("else") &&
                    Statement()) return true;
                    pos = oldPos;
                    return true;
                    }
                }



                bool IterationStatement()
                { return false; }

                bool ContinueStatement()
                {
                    int oldPos = pos;
                    if (match("continue") && match(";")) return true;
                    pos = oldPos;
                    return false;
                }

                bool BreakStatement()
                {
                    int oldPos = pos;
                    if (match("break") && match(";")) return true;
                    pos = oldPos;
                    return false;
                }

                bool ReturnStatement()
                {
                    int oldPos = pos;
                    if (match("return") &&
                    match(";")) return true;
                    pos = oldPos;

                    if (match("return") &&
                    Expression() &&
                    match(";")) return true;
                    pos = oldPos;

                    return false;
                }
                bool WithStatement()
                {
                    int oldPos = pos;
                    if (match("with") &&
                    match("(") &&
                    Expression() &&
                    match(")") &&
                    Statement()) return true;
                    pos = oldPos;

                    return false;
                }
        */

        Function FunctionDeclaration()
        {
            Block block = null;
            int oldPos = pos;
            string id;

            if (Match("function") &&
            (id = tokens[pos].GetTokenValue()) != null &&
            tokens[pos++].GetTokenType() == TokenType.Identifier &&
            Match("(")){
                List<SimpleExpression> parameters = FormalParameterList();
                if (Match(")") &&
                Match("{") &&
                (block = FunctionBody()) != null &&
                Match("}")
                )
                {
                    return new Function(id, parameters, block);
                }
            }

            pos = oldPos;
            return null;
        }

        Block FunctionBody()
        {
            Block node = new Block();
            int oldPos = pos;
            if ((node.Body = SourseElements()) != null) return node;
            pos = oldPos;
            return null;
        }

        List<SimpleExpression> FormalParameterList() // not by spec
        {
            int oldPos = pos;                        
            string id = "";

            if (tokens[pos].GetTokenType() == TokenType.Identifier &&
                (id = tokens[pos++].GetTokenValue()) != null)
            {
                oldPos = pos;
                List<SimpleExpression> parameters = new List<SimpleExpression>();                
                List<SimpleExpression> next = FormalParameterList1();

                parameters.Add(new SimpleExpression(ExpressionType.Ident, id));
                if (next != null) parameters.AddRange(next);
                return parameters;
            }

            pos = oldPos;
            return null;
        }

        List<SimpleExpression> FormalParameterList1()//e
        {
            int oldPos = pos;
            string id = "";

            if (Match(",") &&
            tokens[pos].GetTokenType() == TokenType.Identifier &&
            (id = tokens[pos++].GetTokenValue()) != null)
            {
                oldPos = pos;
                List<SimpleExpression> parameters = new List<SimpleExpression>();
                List<SimpleExpression> next = FormalParameterList1();

                parameters.Add(new SimpleExpression(ExpressionType.Ident, id));

                if (next != null) parameters.AddRange(next);
                return parameters;
            }
            return null;
        }
    }

}

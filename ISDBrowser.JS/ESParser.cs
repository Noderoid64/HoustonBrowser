using System;

namespace ISDBrowser.JS
{
/*
TODO:
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

    public class ESParser
    {
        Token[] parseString;
        int pos;

        bool match(string expected)
        {
            try
            {
                if (expected == parseString[pos].GetTokenValue()) { pos++; return true; }
                return false;
            }
            catch (System.IndexOutOfRangeException)
            {
                return false;
            }
        }

        public bool Program(Token[] w)
        {
            try
            {
                pos = 0;
                parseString = w;
                return SourseElements();
            }
            catch
            {
                return false;
            }
        }

        bool SourseElements()
        {
            int oldPos = pos;
            if (SourseElement() && SourseElements1()) return true;
            pos = oldPos;
            return false;
        }

        bool SourseElements1()//e
        {
            if (pos == parseString.Length) return true;
            int oldPos = pos;
            if (SourseElement() && SourseElements1()) return true;
            pos = oldPos;
            return true;
        }

        bool SourseElement()
        {
            int oldPos = pos;
            switch (parseString[pos].GetTokenValue())
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
                    throw new Exception("Unexpected token: " + parseString[pos].GetTokenValue());
            }

            if (Statement()) return true;
            pos = oldPos;
            if (FunctionDeclaration()) return true;
            return false;
        }

        bool Statement()
        {
            int oldPos = pos;
            if (Block()) return true;
            pos = oldPos;

            if (VariableStatement()) return true;
            pos = oldPos;

            if (EmptyStatement()) return true;
            pos = oldPos;

            if (ExpressionStatement()) return true;
            pos = oldPos;

            if (IfStatement()) return true;
            pos = oldPos;

            // backtrack();
            // saveCursor();
            // IterationStatement();

            if (ContinueStatement()) return true;
            pos = oldPos;

            if (BreakStatement()) return true;
            pos = oldPos;

            if (ReturnStatement()) return true;
            pos = oldPos;

            if (WithStatement()) return true;
            pos = oldPos;
            return false;
        }

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

        bool VariableStatement()
        {
            int oldPos = pos;
            if (match("var") && VariableDeclarationList() && match(";")) return true;
            pos = oldPos;
            return false;
        }

        bool VariableDeclarationList()
        {
            int oldPos = pos;
            if (VariableDeclaration() && VariableDeclarationList1()) return true;
            pos = oldPos;
            return false;
        }

        bool VariableDeclarationList1()//e
        {
            int oldPos = pos;
            if (match(",") && VariableDeclaration() && VariableDeclarationList1()) return true;
            pos = oldPos;
            return true;
        }

        bool VariableDeclaration()
        {
            int oldPos = pos;
            if (parseString[pos++].GetTokenType() == TokenType.Identifier && Initializer()) return true;
            pos = oldPos;
            if (parseString[pos++].GetTokenType() == TokenType.Identifier) return true;
            pos = oldPos;
            return false;
        }

        bool Initializer()
        {
            int oldPos = pos;
            if (match("=") && AssignmentExpression()) return true;
            pos = oldPos;
            return false;
        }

        bool AssignmentExpression()
        {
            int oldPos = pos;
            if (LeftHandSideExpression() &&
            AssignmentOperator() &&
            AssignmentExpression()) return true;
            pos = oldPos;
            if (ConditionalExpression()) return true;
            pos = oldPos;
            return false;
        }

        bool AssignmentOperator()
        {
            switch (parseString[pos].GetTokenValue())
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
                    match(parseString[pos].GetTokenValue());
                    return true;
            }
            return false;
        }

        bool ConditionalExpression()
        {
            int oldPos = pos;
            if (LogicalORExpression() &&
            match("?") &&
            AssignmentExpression() &&
            match(":") &&
            AssignmentExpression()) return true;
            pos = oldPos;
            if (LogicalORExpression()) return true;
            pos = oldPos;
            return false;
        }

        #region Logical expressions
        bool LogicalORExpression()
        {
            int oldPos = pos;
            if (LogicalANDExpression() && LogicalORExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool LogicalORExpression1()//e
        {
            int oldPos = pos;
            if (match("||") &&
            LogicalANDExpression() &&
            LogicalORExpression1()) return true;
            pos = oldPos;
            return true;
        }

        bool LogicalANDExpression()
        {
            int oldPos = pos;
            bool res = BitwiseORExpression() &&
            LogicalANDExpression1();
            if (res) return true;
            pos = oldPos;
            return false;
        }

        bool LogicalANDExpression1()//e
        {
            int oldPos = pos;
            if (match("&&") &&
            BitwiseORExpression() &&
            LogicalANDExpression1()) return true;
            pos = oldPos;
            return true;
        }

        bool BitwiseORExpression()
        {
            int oldPos = pos;
            if (BitwiseXORExpression() && BitwiseORExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool BitwiseORExpression1()//e
        {
            int oldPos = pos;
            if (match("|") &&
            BitwiseXORExpression() &&
            BitwiseORExpression1()) return true;
            pos = oldPos;
            return true;
        }

        bool BitwiseXORExpression()
        {
            int oldPos = pos;
            if (BitwiseANDExpression() && BitwiseXORExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool BitwiseXORExpression1()//e
        {
            int oldPos = pos;
            if (match("^") &&
            BitwiseANDExpression() &&
            BitwiseXORExpression1()) return true;
            pos = oldPos;
            return true;
        }

        bool BitwiseANDExpression()
        {
            int oldPos = pos;
            if (EqualityExpression() && BitwiseANDExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool BitwiseANDExpression1()//e
        {
            int oldPos = pos;
            if (match("&") &&
            EqualityExpression() &&
            BitwiseANDExpression1()) return true;
            pos = oldPos;
            return true;
        }
        #endregion
        bool EqualityExpression()
        {
            int oldPos = pos;
            if (RelationalExpression() && EqualityExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool EqualityExpression1()//e
        {
            int oldPos = pos;
            bool res = match("==") &&
            RelationalExpression() &&
            EqualityExpression1();
            if (res) return true;
            pos = oldPos;
            res = match("!=") &&
            RelationalExpression() &&
            EqualityExpression1();
            if (res) return true;
            pos = oldPos;
            return true;
        }

        bool RelationalExpression()
        {
            int oldPos = pos;
            bool res = ShiftExpression() &&
            RelationalExpression1();
            if (res) return true;
            pos = oldPos;
            return false;
        }

        bool RelationalExpression1()//e
        {
            int oldPos = pos;
            switch (parseString[pos].GetTokenValue())
            {
                case ">":
                case "<":
                case ">=":
                case "<=":
                case "in":
                case "instanseof":
                    if (match(parseString[pos].GetTokenValue()) &&
                    ShiftExpression() &&
                    RelationalExpression1()) return true;
                    break;

            }
            pos = oldPos;
            return true;
        }

        bool ShiftExpression()
        {
            int oldPos = pos;
            if (AdditiveExpression() && ShiftExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool ShiftExpression1()//e
        {
            switch (parseString[pos].GetTokenValue())
            {
                case "<<":
                case ">>":
                case ">>>":
                    if (match(parseString[pos].GetTokenValue()) &&
                    AdditiveExpression() &&
                    ShiftExpression1()) return true;
                    break;
            }
            return true;
        }

        bool AdditiveExpression()
        {
            int oldPos = pos;
            if (MultiplicativeExpression() && AdditiveExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool AdditiveExpression1()//e
        {
            int oldPos = pos;
            switch (parseString[pos].GetTokenValue())
            {
                case "+":
                case "-":
                    if (match(parseString[pos].GetTokenValue()) &&
                    MultiplicativeExpression() &&
                    AdditiveExpression1()) return true;
                    break;
            }
            pos = oldPos;
            return true;
        }

        bool MultiplicativeExpression()
        {
            int oldPos = pos;
            if (UnaryExpression() && MultiplicativeExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool MultiplicativeExpression1()//e
        {
            int oldPos = pos;
            switch (parseString[pos].GetTokenValue())
            {
                case "*":
                case "/":
                case "%":
                    if (match(parseString[pos].GetTokenValue()) &&
                    UnaryExpression() &&
                    MultiplicativeExpression1()) return true;
                    break;
            }
            pos = oldPos;
            return true;
        }

        bool UnaryExpression()
        {
            int oldPos = pos;
            switch (parseString[pos].GetTokenValue())
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
                    if (match(parseString[pos].GetTokenValue()) &&
                    UnaryExpression()) return true;
                    break;
            }
            pos = oldPos;
            if (PostfixExpression()) return true;
            return false;
        }

        bool PostfixExpression()
        {
            int oldPos = pos;

            if (LeftHandSideExpression() && match("++")) return true;
            pos = oldPos;
            if (LeftHandSideExpression() && match("--")) return true;
            pos = oldPos;
            if (LeftHandSideExpression()) return true;
            oldPos = pos;
            return false;
        }

        bool LeftHandSideExpression()
        {
            int oldPos = pos;
            if (NewExpression()) return true;
            pos = oldPos;
            if (CallExpression()) return true;
            pos = oldPos;
            return false;
        }

        bool NewExpression()
        {
            int oldPos = pos;
            if (match("new") && NewExpression()) return true;
            pos = oldPos;
            if (MemberExpression()) return true;
            pos = oldPos;
            return false;
        }

        bool MemberExpression()
        {
            int oldPos = pos;
            if (match("new") && MemberExpression() &&
            Arguments() &&
            MemberExpression1()) return true;
            pos = oldPos;
            if (PrimaryExpression() &&
            MemberExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool MemberExpression1()//e
        {
            int oldPos = pos;

            switch (parseString[pos].GetTokenValue())
            {
                case "[":
                    if (match("[") && Expression() && match("]") && MemberExpression1()) return true;
                    break;
                case ".":
                    if (match(".") &&
                    parseString[pos++].GetTokenType() == TokenType.Identifier &&
                    MemberExpression1()) return true;
                    break;
            }
            pos = oldPos;
            return true;
        }

        bool PrimaryExpression()
        {
            int oldPos = pos;
            if (match("this")) return true;
            pos = oldPos;
            if (parseString[pos++].GetTokenType() == TokenType.Identifier) return true;
            pos = oldPos;
            if (parseString[pos++].GetTokenType() == TokenType.Literal) return true;
            pos = oldPos;
            if (ObjectLiteral()) return true;
            pos = oldPos;
            // TODO: Array literal


            if (match("(") && Expression() && match(")")) return true;
            pos = oldPos;
            return false;
        }

        bool ObjectLiteral()
        {
            int oldPos = pos;
            if (match("{") && match("}")) return true;
            pos = oldPos;
            if (match("{") && PropertyNameAndValueList() && match("}")) return true;
            pos = oldPos;
            return false;
        }
        bool PropertyNameAndValueList()
        {
            int oldPos = pos;
            if (PropertyName() && match(":") && AssignmentExpression() && PropertyNameAndValueList1()) return true;
            pos = oldPos;
            return false;
        }

        bool PropertyNameAndValueList1()//e
        {
            int oldPos = pos;
            if (match(",") &&
            PropertyName() &&
            match(":") &&
            AssignmentExpression() &&
            PropertyNameAndValueList1()) return true;
            pos = oldPos;
            return true;
        }

        bool PropertyName()
        {
            int oldPos = pos;
            if (parseString[pos++].GetTokenType() == TokenType.Identifier) return true;
            pos = oldPos;
            if (parseString[pos++].GetTokenType() == TokenType.StringLiteral) return true;
            pos = oldPos;
            if (parseString[pos++].GetTokenType() == TokenType.NumericLiteral) return true;
            pos = oldPos;
            return false;
        }



        bool Expression()
        {
            int oldPos = pos;
            if (AssignmentExpression() && Expression1()) return true;
            pos = oldPos;
            return false;
        }

        bool Expression1()//e
        {
            int oldPos = pos;
            if (match(",") && AssignmentExpression() && Expression1()) return true;
            pos = oldPos;
            return true;
        }

        bool Arguments()
        {
            int oldPos = pos;
            if (match("(") && match(")")) return true;
            pos = oldPos;
            if (match("(") && ArgumentList() && match(")")) return true;
            pos = oldPos;
            return false;
        }

        bool ArgumentList()
        {
            int oldPos = pos;
            if (AssignmentExpression() && ArgumentList1()) return true;
            pos = oldPos;
            return false;
        }

        bool ArgumentList1()//e
        {
            int oldPos = pos;
            if (match(",") && AssignmentExpression() && ArgumentList1()) return true;
            pos = oldPos;
            return true;
        }

        bool CallExpression()
        {
            int oldPos = pos;
            if (MemberExpression() && Arguments() && CallExpression1()) return true;
            pos = oldPos;
            return false;
        }

        bool CallExpression1()//e
        {
            int oldPos = pos;
            if (Arguments() && CallExpression1()) return true;
            pos = oldPos;
            if (match("[") && Expression() && match("]") && CallExpression1()) return true;
            pos = oldPos;
            if (match(".") && parseString[pos++].GetTokenType() == TokenType.Identifier && CallExpression1()) return true;
            pos = oldPos;
            return true;
        }

        bool EmptyStatement()
        {
            int oldPos = pos;
            if (match(";")) return true;
            pos = oldPos;
            return false;
        }

        bool ExpressionStatement()
        {
            int oldPos = pos;
            if (Expression() && match(";")) return true;
            pos = oldPos;
            return false;
        }

        bool IfStatement()
        {
            int oldPos = pos;
            bool firstPart = match("if") && match("(") &&
            Expression() && match(")") && Statement();
            if (firstPart)
            {
                oldPos = pos;
                if (firstPart && match("else") &&
                Statement()) return true;
                pos = oldPos;
                return true;
            }
            pos = oldPos;
            return false;
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

        bool FunctionDeclaration()
        {
            int oldPos = pos;
            if (match("function") &&
            parseString[pos++].GetTokenType() == TokenType.Identifier &&
            match("(") &&
            match(")") &&
            match("{") &&
            FunctionBody() &&
            match("}")
            ) return true;
            pos = oldPos;

            if (match("function") &&
            parseString[pos++].GetTokenType() == TokenType.Identifier &&
            match("(") &&
            FormalParameterList() &&
            match(")") &&
            match("{") &&
            FunctionBody() &&
            match("}")
            ) return true;
            pos = oldPos;
            return false;
        }

        bool FunctionBody()
        {
            int oldPos = pos;
            if (SourseElements()) return true;
            pos = oldPos;
            return false;
        }

        bool FormalParameterList()
        {
            int oldPos = pos;
            if (parseString[pos++].GetTokenType() == TokenType.Identifier &&
            FormalParameterList1()) return true;
            pos = oldPos;
            return false;
        }

        bool FormalParameterList1()//e
        {
            int oldPos = pos;
            if (match(",") &&
            parseString[pos++].GetTokenType() == TokenType.Identifier &&
            FormalParameterList1()) return true;
            pos = oldPos;
            return true;
        }
    }
}

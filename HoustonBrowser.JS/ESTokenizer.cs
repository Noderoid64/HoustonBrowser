using System;
using System.Collections.Generic;

namespace HoustonBrowser.JS
{
    class ESTokenizer
    {       
        List<Token> tokens = new List<Token>();
        string parsingString;
        int pos=0;
        int savedPos = 0;

        int SavePos()
        {
            return savedPos = pos;
        }

        void RestorePos()
        {
            pos = savedPos;
        }

        bool Match(char expected)
        {
            try
            {
                if (expected == parsingString[pos]) { pos++; return true; }
                return false;
            }
            catch (System.IndexOutOfRangeException)
            {
                return false;
            }
        }
        
        bool MatchSequence(string expected)
        {
            try
            {
                for (int i = 0; i < expected.Length; i++)
                {
                    if(expected[i]!=parsingString[pos])return false;
                    pos++;
                }
                return true; 
            }
            catch (System.IndexOutOfRangeException)
            {
                return false;
            }
        }       

        bool MatchInterval(char expectedLeft, char expectedRight)
        {
            try
            {
                int res = (parsingString[pos]-expectedLeft)*(parsingString[pos]-expectedRight);
                if( res <= 0)
                {
                    pos++;
                    return true;
                }
                return false;
            }
            catch (System.IndexOutOfRangeException)
            {                
                return false;
            }
        }


        public List<Token> Tokenize(string data)
        {
            pos=0;
            parsingString = data;
            tokens = new List<Token>();

            Root();

            return tokens;
        }

        bool WhiteSpace()
        {
            int startingPos = SavePos();
            while(Match('\u0009')
            || Match('\u000B')
            || Match('\u000C')
            || Match('\u0020')
            || Match('\u00A0')
            || Match(' ')){}
            if(startingPos == pos)return false;
            return true;           
        }

        bool LineTerminator()
        {
            int startingPos = SavePos();
            while(Match('\u000A')
            || Match('\u000D')
            || Match('\u2028')
            || Match('\u2029')){}
            if(startingPos == pos)return false;
            return true;
        }

        void Root()
        {
            while (pos<parsingString.Length)
            {       
                Token();
                WhiteSpace();
                LineTerminator();

            }
        }

        bool Token()
        {       
            if(ReservedWord()
            || Identifier()
            || Punctuator()
            || NumericLiteral()
            || StringLiteral()
            ) return true; 
            
            return false;
        }

        private bool NumericLiteral()
        {            
            int startingPos = SavePos();
            while (MatchInterval('0', '9')) { }
            if (Match('.')) while (MatchInterval('0', '9')) { }


            if (startingPos == pos) return false;
            else
            {
                tokens.Add(new Token(TokenType.NumericLiteral, parsingString.Substring(startingPos, pos - startingPos)));
                return true;
            }
        }

        bool Identifier()
        {
            string temp="";
            if((temp=IdentifierName())!=null){ 
                tokens.Add(new Token(TokenType.Identifier,temp));
                return true;
            }
            return false;
        }

        string IdentifierName() //not by specification
        {
            int startingPos = SavePos();
            while(MatchInterval('a','z') 
            || MatchInterval('A','Z') 
            || Match('$')
            || Match('_')){}
            if(startingPos == pos)return null;
            else return parsingString.Substring(startingPos,pos-startingPos);
        }

        bool StringLiteral() //not by specification
        {
            int startingPos = SavePos();
            if(Match('"'))
            {
                while(!Match('\r')
                     && !Match('\n')
                     && !Match('"')) { pos++; }
            }else if(Match('\''))
            {
                while (!Match('\r')
                 && !Match('\n')
                 && !Match('\'')) { pos++; }
            }
            if(startingPos == pos)return false;

            tokens.Add(new Token(TokenType.StringLiteral,parsingString.Substring(startingPos+1,pos-startingPos-2)));
            return true;
        }

        bool IdentifierStart()
        {
            throw new NotImplementedException();
        }

        bool ReservedWord()
        {
            string[] keywords = {"function","if","else","var","this","new","return"};
            SavePos();
            if (MatchSequence("null"))
            {
                tokens.Add(new Token(TokenType.NullLiteral, "null"));
                return true;
            }
            RestorePos();
            if (MatchSequence("true"))
            {
                tokens.Add(new Token(TokenType.BooleanLiteral, "true"));
                return true;
            }
            RestorePos();
            if (MatchSequence("false"))
            {
                tokens.Add(new Token(TokenType.BooleanLiteral, "false"));
                return true;
            }
            RestorePos();

            foreach (var item in keywords)
            {
                SavePos();
                if(MatchSequence(item))
                {
                    tokens.Add(new Token(TokenType.Keyword,item));
                    return true;
                }
                RestorePos();
            }
            RestorePos();
            return false;
        }
    
        bool Punctuator()
        {
            string[] keywords = {"{","}","(",")",".",";","=",",","||","&&","+","-","*","/"};
            
            foreach (var item in keywords)
            {
                SavePos();
                if(MatchSequence(item))
                {
                    tokens.Add(new Token(TokenType.Punctuator,item));
                    return true;
                }
                RestorePos();
            }
            return false;


        }
    }


}
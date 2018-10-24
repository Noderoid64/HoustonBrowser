namespace ISDBrowser.JS
{
    public class Token
    {
        TokenType type;
        string value;

        public TokenType GetTokenType() => type;
        public string GetTokenValue() => value;
        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;    
        }
    }
}
namespace IDE.Model.Parser.States
{
    internal class PreIdentifierWhitespaceState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if(token.Type == TokenType.Whitespace)
            {
                parser.State = new IdentifierState();
                return true;
            }

            return false;
        }
    }
}

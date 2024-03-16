namespace IDE.Model.Parser.States
{
    internal class PreIdentifierWhitespaceState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if(token.Type == TokenType.Whitespace)
            {
                parser.State = new IdentifierState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

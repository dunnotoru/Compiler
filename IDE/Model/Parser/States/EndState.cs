namespace IDE.Model.Parser.States
{
    internal class EndState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type != TokenType.Semicolon)
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

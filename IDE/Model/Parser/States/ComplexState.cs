namespace IDE.Model.Parser.States
{
    internal class ComplexState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.Complex)
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

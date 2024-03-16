namespace IDE.Model.Parser.States
{
    internal class IdentifierState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if(token.Type == TokenType.Identifier)
            {
                parser.State = new OpenArgumentState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

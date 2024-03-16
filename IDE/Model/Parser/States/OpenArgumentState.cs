namespace IDE.Model.Parser.States
{
    internal class OpenArgumentState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.OpenRoundBracket)
            {
                parser.State = new FirstNumberState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

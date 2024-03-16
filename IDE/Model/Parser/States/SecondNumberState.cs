namespace IDE.Model.Parser.States
{
    internal class SecondNumberState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.DoubleLiteral)
            {
                parser.State = new CloseArgumentsState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

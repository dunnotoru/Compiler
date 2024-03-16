namespace IDE.Model.Parser.States
{
    internal class FirstNumberState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.DoubleLiteral)
            {
                parser.State = new ArgumentSplitterState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

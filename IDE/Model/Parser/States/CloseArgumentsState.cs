namespace IDE.Model.Parser.States
{
    internal class CloseArgumentsState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.CloseRoundBracket)
            {
                parser.State = new EndState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

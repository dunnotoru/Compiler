namespace IDE.Model.Parser.States
{
    internal class CloseArgumentsState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.CloseRoundBracket)
            {
                parser.State = new EndState();
                return true;
            }

            return false;
        }
    }
}

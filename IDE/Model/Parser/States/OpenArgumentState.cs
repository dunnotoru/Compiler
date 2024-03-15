namespace IDE.Model.Parser.States
{
    internal class OpenArgumentState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.OpenRoundBracket)
            {
                parser.State = new FirstNumberState();
                return true;
            }

            return false;
        }
    }
}

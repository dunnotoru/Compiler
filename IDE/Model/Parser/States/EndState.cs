namespace IDE.Model.Parser.States
{
    internal class EndState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.Semicolon)
            {
                return true;
            }

            return false;
        }
    }
}

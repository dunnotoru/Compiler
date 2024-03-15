namespace IDE.Model.Parser.States
{
    internal class SecondNumberState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.DoubleLiteral)
            {
                parser.State = new CloseArgumentsState();
                return true;
            }

            return false;
        }
    }
}

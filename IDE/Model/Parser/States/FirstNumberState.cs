namespace IDE.Model.Parser.States
{
    internal class FirstNumberState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.DoubleLiteral)
            {
                parser.State = new ArgumentSplitterState();
                return true;
            }

            return false;
        }
    }
}

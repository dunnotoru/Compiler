namespace IDE.Model.Parser.States
{
    internal class IdentifierState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if(token.Type == TokenType.Identifier)
            {
                parser.State = new OpenArgumentState();
                return true;
            }

            return false;
        }
    }
}

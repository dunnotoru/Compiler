using System;

namespace IDE.Model.Parser.States
{
    internal class ComplexState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.Complex)
            {
                parser.State = new IdentifierState();
                return true;
            }

            return false;
        }
    }
}

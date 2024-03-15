using System;

namespace IDE.Model.Parser.States
{
    internal class ArgumentSplitterState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.Comma)
            {
                parser.State = new SecondNumberState();
                return true;
            }

            return false;
        }
    }
}

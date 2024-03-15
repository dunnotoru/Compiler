using System;

namespace IDE.Model.Parser.States
{
    internal class ErrorState : IParserState
    {
        public bool Handle(Parser parser, Token token)
        {
            return false;
        }
    }
}

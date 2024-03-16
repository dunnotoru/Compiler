using System;

namespace IDE.Model.Parser.States
{
    internal class ArgumentSplitterState : IParserState
    {
        public void Handle(Parser parser, Token token)
        {
            if (token.Type == TokenType.Comma)
            {
                parser.State = new SecondNumberState();
            }
            else
            {
                parser.Errors.Add(new ParseError(token));
            }
        }
    }
}

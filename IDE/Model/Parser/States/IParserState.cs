using System.Collections.Generic;

namespace IDE.Model.Parser.States
{
    internal interface IParserState
    {
        void Parse(Parser parser, List<Token> tokens, List<IParserState> states);
    }
}

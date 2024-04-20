using System.Collections.Generic;

namespace IDE.Model.Parser.States
{
    internal interface IParserState
    {
        bool Parse(Parser parser, List<Token> tokens, List<IParserState> states);
    }
}

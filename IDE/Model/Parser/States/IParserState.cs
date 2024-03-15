namespace IDE.Model.Parser.States
{
    internal interface IParserState
    {
        bool Handle(Parser parser, Token token);
    }
}

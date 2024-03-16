namespace IDE.Model.Parser.States
{
    internal interface IParserState
    {
        void Handle(Parser parser, Token token);
    }
}

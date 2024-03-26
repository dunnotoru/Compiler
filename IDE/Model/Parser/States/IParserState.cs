namespace IDE.Model.Parser.States
{
    internal interface IParserState
    {
        string Handle(Parser parser, string code, int position);
    }
}

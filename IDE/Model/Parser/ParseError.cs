namespace IDE.Model.Parser
{
    internal class ParseError
    {
        public string Actual { get; private set; }
        public string Expected { get; private set; }
        public int Pos { get; private set; }

        public ParseError(int pos, string actual, string expected)
        {
            Pos = pos;
            Actual = actual;
            Expected = expected;
        }
    }
}

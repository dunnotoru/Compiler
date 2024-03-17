namespace IDE.Model.Parser
{
    internal class ParseError
    {
        public string ExpectedTokenType { get; private set; }
        public string DiscardedFragment { get; private set; }
        public int StartPos { get; private set; }
        public int EndPos { get; private set; }

        public ParseError(int startPosition, int endPosition, string expected, string discardedFragment)
        {
            ExpectedTokenType = expected;
            DiscardedFragment = discardedFragment;
            StartPos = startPosition;
            EndPos = endPosition;
        }
    }
}

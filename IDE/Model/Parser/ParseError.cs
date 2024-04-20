using System.Collections.Generic;

namespace IDE.Model.Parser
{
    internal class ParseError
    {
        private List<Token> errors;

        public string Actual { get; private set; }
        public string Expected { get; private set; }
        public int Pos { get; private set; }

        public ParseError(int pos, string actual, string expected)
        {
            Pos = pos;
            Actual = actual;
            Expected = expected;
        }

        public ParseError(List<Token> errors, string expected)
        {
            this.errors = errors;
            Expected = expected;
        }
    }
}

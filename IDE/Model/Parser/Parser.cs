using IDE.Model.Parser.States;
using System.Collections.Generic;

namespace IDE.Model.Parser
{
    internal class Parser
    {
        public IParserState State { get; set; }

        public Parser()
        {
            State = new ComplexState();
        }

        public void Parse(IEnumerable<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                State.Handle(this, token);
            }


        }
    }
}

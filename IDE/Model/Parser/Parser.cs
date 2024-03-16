using IDE.Model.Parser.States;
using System.Collections.Generic;

namespace IDE.Model.Parser
{
    internal class Parser
    {
        public IParserState State { get; set; }
        public List<ParseError> Errors { get; set; }
        public Parser()
        {
            Errors = new List<ParseError>();
            State = new ComplexState();
        }

        public List<ParseError> Parse(IEnumerable<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                State.Handle(this, token);
            
            }

            return Errors;
        }
    }
}

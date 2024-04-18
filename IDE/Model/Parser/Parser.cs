using IDE.Model.Parser.States;
using System.Collections.Generic;

namespace IDE.Model.Parser
{
    internal class Parser
    {
        public IParserState State { get; set; } = new ComplexState();
        private List<ParseError> Errors { get; set; } = new List<ParseError>();

        public List<ParseError> Parse(List<Token> tokens)
        {
            Errors.Clear();
            State = new ComplexState();
            State.Parse(this, tokens);
            return Errors;
        }

        public void AddError(ParseError error)
        {
            Errors.Add(error);
        }
    }
}

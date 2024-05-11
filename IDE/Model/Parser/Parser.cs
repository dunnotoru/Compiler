using IDE.Model.Parser.States;
using System.Collections.Generic;

namespace IDE.Model.Parser
{
    internal class Parser
    {
        public IParserState State { get; set; }
        private List<ParseError> Errors { get; set; }
        public Parser()
        {
            Errors = new List<ParseError>();
            State = new ComplexState();
        }

        public (List<ParseError>, string) Parse(string code)
        {
            Errors.Clear();
            State = new ComplexState();
            string resultCode = State.Handle(this, code, 0);
            return (Errors, resultCode);
        }

        public void AddError(ParseError error)
        {
            Errors.Add(error);
        }
    }
}

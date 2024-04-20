using IDE.Model.Parser.States;
using System.Collections.Generic;
using System.Linq;

namespace IDE.Model.Parser
{
    internal class Parser
    {
        private List<ParseError> Errors { get; set; } = new List<ParseError>();
        

        public List<ParseError> Parse(List<Token> tokens)
        {
            List<IParserState> States = new List<IParserState>()
            {
                new ComplexState(),
                new IdentifierState(),
                new OpenParenthesisState(),
                new RealPartState(),
                new CommaState(),
                new ImaginaryPartState(),
                new CloseParenthesisState(),
                new SemicolonState(),
            };
            Errors.Clear();
            States.First().Parse(this, tokens, States.ToList());
            return Errors;
        }

        public void AddError(ParseError error)
        {
            Errors.Add(error);
        }

        
    }
}

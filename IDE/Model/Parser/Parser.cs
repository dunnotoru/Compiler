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

            List<Token> line = tokens.TakeWhile(t => t.Type != TokenType.Newline).ToList();
            tokens = tokens.SkipWhile(t => t.Type != TokenType.Newline).ToList();
            tokens = tokens.SkipWhile(t => t.Type == TokenType.Newline).ToList();
            while (line.Count > 0)
            {
                States.First().Parse(this, line, States.ToList());
                if (line.Last().Type != TokenType.Semicolon)
                {
                    ParserUtils.CreateError(this, line.Last().EndPos, "Незаконченное выражение");
                }
                line = tokens.TakeWhile(t => t.Type != TokenType.Newline).ToList();
                tokens = tokens.SkipWhile(t => t.Type != TokenType.Newline).ToList();
                tokens = tokens.SkipWhile(t => t.Type == TokenType.Newline).ToList();
            }

            return Errors;
        }

        public void AddError(ParseError error)
        {
            Errors.Add(error);
        }
    }
}

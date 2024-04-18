using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDE.Model.Parser.States
{
    internal class ComplexState : IParserState
    {
        public void Parse(Parser parser, List<Token> tokens)
        {
            Token? lastToken = null;
            List<Token> ErrorBuffer = new List<Token>();
            foreach (Token token in tokens.ToList())
            {
                if (tokens.Count == 0)
                {
                    break;
                }

                if (token.Type == TokenType.Complex)
                {
                    tokens.Remove(token);
                    break;
                }
                else if (token.Type != TokenType.Whitespace)
                {
                    ErrorBuffer.Add(token);
                    tokens.Remove(token);
                    break;
                }

                lastToken = token;
                tokens.Remove(token);
            }

            if (ErrorBuffer.Count > 0) { 
                StringBuilder sb = new StringBuilder();
                foreach (Token token in ErrorBuffer)
                {
                    sb.Append(token.RawToken + " ");
                }
                parser.AddError(new ParseError(ErrorBuffer.First().StartPos, sb.ToString(), "std::complex<double>"));
            }

            if (tokens.Count == 0)
            {
                if (lastToken != null)
                {
                    parser.AddError(new ParseError(lastToken.EndPos, "", "Unfinished string"));
                }
                return;
            }

            parser.State = new IdentifierState();
            parser.State.Parse(parser, tokens);
        }
    }
}

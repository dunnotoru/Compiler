using System.Collections.Generic;
using System.Linq;

namespace IDE.Model.Parser.States
{
    internal class OpenParenthesisState : IParserState
    {
        public void Parse(Parser parser, List<Token> tokens, List<IParserState> states)
        {
            if (ParserUtils.TrimWhitespaceTokens(ref tokens) == false || states.Count == 0)
            {
                return;
            }

            List<Token> tail = new List<Token>(tokens);
            List<Token> errorBuffer = new List<Token>();
            foreach (Token token in tail.ToList())
            {
                if (token.Type == TokenType.DoubleLiteral)
                {
                    if (token == tokens.First() && errorBuffer.Count == 0)
                    {
                        ParserUtils.CreateError(parser, token.StartPos, "missing open br");
                    }
                    break;
                }
                if (token.Type != TokenType.OpenParenthesis)
                {
                    errorBuffer.Add(token);
                    tail.Remove(token);
                }
                else
                {
                    tail.Remove(tail.First());
                    break;
                }
            }

            states = states.Skip(1).ToList();
            if (tail.Count > 0)
            {
                ParserUtils.CreateErrorFromBuffer(parser, errorBuffer, "open br");
                states.FirstOrDefault()?.Parse(parser, tail, states);
                return;
            }

            states.FirstOrDefault()?.Parse(parser, tail, states);
        }
    }
}
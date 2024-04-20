﻿using System.Collections.Generic;
using System.Linq;

namespace IDE.Model.Parser.States
{
    internal class ComplexState : IParserState
    {
        public bool Parse(Parser parser, List<Token> tokens, List<IParserState> states)
        {
            if (ParserUtils.TrimWhitespaceTokens(ref tokens) == false)
            {
                return true;
            }
            if (states.Count == 0)
            {
                return false;
            }
            states.Remove(states.First());

            List<Token> tail = new List<Token>(tokens);
            List<Token> errorBuffer = new List<Token>();
            foreach (Token token in tail.ToList())
            {
                if (token.Type == TokenType.Identifier)
                {
                    if (token == tokens.First() && errorBuffer.Count == 0)
                    {
                        ParserUtils.CreateError(parser, token.StartPos, "missing std::complex<double>");
                    }
                    break;
                }
                if (token.Type != TokenType.Complex)
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


            if (tail.Count > 0 && states.Count != 0)
            {
                ParserUtils.CreateErrorFromBuffer(parser, errorBuffer, "std::complex<double>");
                return states.First().Parse(parser, tail, states);
            }
            else if (states.Count != 0)
            {
                return states.First().Parse(parser, tokens, states);
            }

            return false;
        }
    }
}

using IDE.Model;
using IDE.Model.Parser;
using IDE.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDE.Services
{
    internal class DescentRecurseParserService : IParseService
    {
        private const string _instructions = "+-><,.";
        private List<Token> _tokens;
        private List<Token> _result;
        private List<ParseError> _errors;
        private int _position = 0;
        public (List<ParseError>, List<Token>) Parse(List<Token> tokens)
        {
            if(tokens.Count == 0)
            {
                return (new List<ParseError>(), new List<Token>()); 
            }

            _tokens = tokens;
            _errors = new List<ParseError>();
            _result = new List<Token>();
            _position = 0;

            ParseProgram();

            return (_errors, _result);
        }

        private void ParseProgram()
        {
            if (_position >= _tokens.Count)
            {
                return;
            }

            while(_position < _tokens.Count)
            {
                ParseInstr();
            }
        }

        private void ParseInstr()
        {
            if(_position >= _tokens.Count)
            {
                return;
            }

            List<Token> buffer = new List<Token>();
            while(_position < _tokens.Count)
            {
                Token token = _tokens[_position++];
                
                if(_instructions.Contains(token.RawToken))
                {
                    buffer.Add(token);
                }
                else if (token.RawToken == "[")
                {
                    ParseInstr();

                    if(_position >= _tokens.Count)
                    {
                        if (buffer.Count > 0)
                        {
                            _result.Add(MergeTokens(buffer));
                        }
                        break;
                    }

                    token = _tokens[_position - 1];
                    _position++;
                    if(token.RawToken != "]")
                    {
                        _errors.Add(new ParseError(_position, _position, "expected ]", $"{token.RawToken}"));
                    }
                    break;
                }
                else if (token.RawToken == "]")
                {
                    break;
                }
                else
                {
                    _errors.Add(new ParseError(_position, _position, "", "invalid symbol met"));
                }
            }

            if (buffer.Count > 0)
            {
                _result.Add(MergeTokens(buffer));
            }
        }

        private Token MergeTokens(List<Token> tokens)
        {
            int start = tokens.First().StartPos;
            StringBuilder buffer = new StringBuilder();
            foreach (Token token in tokens)
            {
                buffer.Append(token.RawToken);   
            }

            return new Token(buffer.ToString(), start);
        }
    }
}

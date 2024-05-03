using IDE.Model;
using IDE.Services.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IDE.Services
{
    internal class TetradService : ITetradService
    {
        private Dictionary<TokenType, string> tetradOp = new Dictionary<TokenType, string>()
        {
            { TokenType.Multiply, "multiply" },
            { TokenType.Divide, "divide" },
            { TokenType.Module, "mod" },
            { TokenType.Plus, "plus" },
            { TokenType.Minus, "minus" },
            { TokenType.Assignment, "assign" }
        };

        public List<Tetrad> GetTetrads(List<Token> tokens)
        {
            return ParseTetrad(tokens, new List<Tetrad>());
        }

        private List<Tetrad> ParseTetrad(List<Token> tokens, List<Tetrad> tetrads)
        {
            ArgumentNullException.ThrowIfNull(nameof(tokens));

            tokens.RemoveAll(_ => string.IsNullOrWhiteSpace(_.RawToken));

            tokens = SearchParenthesis(tokens, tetrads);

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.Minus && (i == 0 || tetradOp.ContainsKey(tokens[i - 1].Type)))
                {
                    Tetrad tetrad = new Tetrad(tetradOp[TokenType.Minus], tokens[i + 1].RawToken, "", "t" + tetrads.Count);
                    tetrads.Add(tetrad);
                    tokens[i] = new Token(tetrad.Result, tokens[i].StartPos);
                    tokens.RemoveAt(i + 1);
                    tetrads = ParseTetrad(tokens, tetrads);
                    return tetrads;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.Multiply
                    || tokens[i].Type == TokenType.Divide
                    || tokens[i].Type == TokenType.Module)
                {
                    string operand1 = tokens[i - 1].RawToken;
                    string operand2 = tokens[i + 1].RawToken;
                    Tetrad createdTetrad = new Tetrad(tetradOp[tokens[i].Type], operand1, operand2, "t" + tetrads.Count);
                    tetrads.Add(createdTetrad);
                    tokens[i - 1] = new Token(createdTetrad.Result, tokens[i - 1].StartPos);
                    tokens.RemoveAt(i);
                    tokens.RemoveAt(i);
                    tetrads = ParseTetrad(tokens, tetrads);
                    return tetrads;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.Plus || tokens[i].Type == TokenType.Minus)
                {
                    string operand1 = tokens[i - 1].RawToken;
                    string operand2 = tokens[i + 1].RawToken;
                    Tetrad createdTetrad = new Tetrad(tetradOp[tokens[i].Type], operand1, operand2, "t" + tetrads.Count);
                    tetrads.Add(createdTetrad);
                    tokens[i - 1] = new Token(createdTetrad.Result, tokens[i - 1].StartPos);
                    tokens.RemoveAt(i);
                    tokens.RemoveAt(i);
                    tetrads = ParseTetrad(tokens, tetrads);
                    return tetrads;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].RawToken == "=" && tokens.Count == 3)
                {
                    string operand1 = tokens[i - 1].RawToken;
                    string operand2 = tokens[i + 1].RawToken;
                    tetrads.Add(new Tetrad(tetradOp[tokens[i].Type], operand2, "", operand1));
                    tokens[i - 1] = new Token(tetrads.Last().Result, tokens[i - 1].StartPos);
                    tokens.RemoveAt(i);
                    tokens.RemoveAt(i);
                    return tetrads;
                }
            }

            return tetrads;
        }

        private List<Token> SearchParenthesis(List<Token> tokens, List<Tetrad> tetrads)
        {
            Stack<Token> stack = new Stack<Token>();
            Token? OpenParenthesis = null;
            Token? CloseParenthesis = null;
            foreach (Token token in tokens)
            {
                if (token.Type == TokenType.OpenParenthesis)
                {
                    if (stack.Count == 0)
                    {
                        OpenParenthesis = token;
                    }
                    stack.Push(token);
                }

                if (token.Type == TokenType.CloseParenthesis)
                {
                    if (stack.Count == 1)
                    {
                        CloseParenthesis = token;
                        stack.Pop();
                    }
                    else if (stack.Count > 1)
                    {
                        stack.Pop();
                    }
                }

                if (OpenParenthesis != null && CloseParenthesis != null)
                {
                    break;
                }
            }

            if (OpenParenthesis != null && CloseParenthesis != null)
            {

                if(tokens.IndexOf(CloseParenthesis) - tokens.IndexOf(OpenParenthesis) < 3)
                {
                    tokens.Remove(CloseParenthesis);
                    tokens.Remove(OpenParenthesis);
                    return tokens;
                }

                List<Token> tokensBuff = new List<Token>(tokens.Skip(tokens.IndexOf(OpenParenthesis) + 1).Take(tokens.IndexOf(CloseParenthesis) - tokens.IndexOf(OpenParenthesis) - 1));
                ParseTetrad(tokensBuff, tetrads);
                
                int startIndex = tokens.IndexOf(OpenParenthesis);
                int endIndex = tokens.IndexOf(CloseParenthesis) + 1;
                tokens.RemoveRange(startIndex, endIndex - startIndex);

                if (tetrads.Count != 0)
                {
                    tokens.Insert(startIndex, new Token(tetrads.Last().Result, OpenParenthesis.StartPos));
                }
                else
                {
                    tokens.Insert(startIndex, new Token(tokensBuff.Last().RawToken, OpenParenthesis.StartPos));
                }
            }

            foreach (Token token in tokens.ToList())
            {
                if (token.Type == TokenType.OpenParenthesis)
                {
                    ParseTetrad(tokens, tetrads);
                }
            }

            return tokens;
        }
    }
}

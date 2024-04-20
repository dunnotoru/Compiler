using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDE.Model
{
    internal class Lexer
    {
        public IEnumerable<Token> Scan(string code)
        {
            if (code.Length == 0)
            {
                return Enumerable.Empty<Token>();
            }

            List<Token> tokens = new List<Token>();
            int position = 0;

            code = code.Replace("\t", "").Replace("\r", "");

            do {
                string rawToken = ParseToken(code, position);
                tokens.Add(new Token(rawToken, position));
                position += rawToken.Length;

            } while (position < code.Length);

            return tokens;
        }

        private string ParseToken(string code, int position)
        {
            char symbol = code[position];
            string allowedIdentifierSymbols = "_:<>";
            if (char.IsWhiteSpace(symbol))
            {
                return symbol.ToString();
            }
            if (char.IsLetter(symbol) || symbol == '_')
            {
                return Parse(code, position, (c) => !char.IsLetterOrDigit(c) && !allowedIdentifierSymbols.Contains(c));
            }
            if (char.IsDigit(symbol))
            {
                return Parse(code, position, (c) => !char.IsLetterOrDigit(c) && c != '.');
            }
            if (symbol == '\"')
            {
                return ParseString(code, position);
            }
            
            return ParseOperator(code, position);
        }

        private string Parse(string code, int position, Func<char, bool> stopRule)
        {
            char symbol;
            StringBuilder buffer = new StringBuilder();
            while (position < code.Length)
            {
                symbol = code[position];
                if (stopRule(symbol))
                {
                    break;
                }

                buffer.Append(symbol);
                position++;
            }

            return buffer.ToString();
        }

        private string ParseString(string code, int position)
        {
            char symbol;
            StringBuilder buffer = new StringBuilder();
            int quotesCount = 0;
            while (position < code.Length)
            {
                symbol = code[position];
                if(symbol == '\"')
                {
                    quotesCount++;
                }
                else if(quotesCount == 2 || symbol == '\n')
                {
                    break;
                }

                buffer.Append(symbol);

                position++;
            }

            return buffer.ToString();
        }

        private string ParseOperator(string code, int position)
        {
            string symbol = code[position].ToString();

            string firstCharacter = "<>=&!|";
            string secondCharacter = "=&|";
            if (position < code.Length - 1)
            {
                if (firstCharacter.Contains(symbol) && secondCharacter.Contains(code[position + 1]))
                {
                    symbol += code[position + 1];
                }
            }

            return symbol;
        }
    }
}

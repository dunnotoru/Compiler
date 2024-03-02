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
            char liter = code[position];
            if (char.IsWhiteSpace(liter))
            {
                return liter.ToString();
            }
            if (char.IsLetter(liter) || liter == '_')
            {
                return Parse(code, position, (liter) => !char.IsLetterOrDigit(liter) && liter != '_');
            }
            if (char.IsDigit(liter))
            {
                return Parse(code, position, (liter) => !char.IsDigit(liter));
            }
            if (liter == '\"')
            {
                return ParseString(code, position);
            }
            
            return ParseOperator(code, position);
        }

        private string Parse(string code, int position, Func<char, bool> stopRule)
        {
            char liter;
            StringBuilder buffer = new StringBuilder();
            while (position < code.Length)
            {
                liter = code[position];
                if (stopRule(liter))
                {
                    break;
                }

                buffer.Append(liter);
                position++;
            }

            return buffer.ToString();
        }

        private string ParseString(string code, int position)
        {
            char liter;
            StringBuilder buffer = new StringBuilder();
            int quotesCount = 0;
            while (position < code.Length)
            {
                liter = code[position];
                if(liter == '\"')
                {
                    quotesCount++;
                }
                else if(quotesCount == 2 || liter == '\n')
                {
                    break;
                }

                buffer.Append(liter);

                position++;
            }

            return buffer.ToString();
        }

        private string ParseOperator(string code, int position)
        {
            string liter = code[position].ToString();

            string firstCharacter = "<>=";
            string secondCharacter = "=";
            if (position < code.Length - 1)
            {
                if (firstCharacter.Contains(liter) && secondCharacter.Contains(code[position + 1]))
                {
                    liter += code[position + 1];
                }
            }

            return liter;
        }
    }
}

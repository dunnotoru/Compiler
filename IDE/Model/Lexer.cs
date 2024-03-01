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
                return Enumerable.Empty<Token>();

            List<Token> tokens = new List<Token>();
            int position = 0;

            code = code.Replace("\t", "").Replace("\r", "");

            string rawToken = "";
            do
            {
                char liter = code[position];

                if (char.IsWhiteSpace(liter))
                {
                    rawToken = liter.ToString();
                }
                else if (char.IsLetter(liter))
                {
                    rawToken = ParseWord(code, position);
                }
                else if (char.IsDigit(liter))
                {
                    rawToken = ParseNumber(code, position);
                }
                else if (liter == '\"')
                {
                    rawToken = ParseString(code, position);
                }
                else
                {
                    rawToken = ParseOperator(code, position);
                }

                tokens.Add(new Token(rawToken, position));
                position += rawToken.Length;

            } while (position < code.Length);

            return tokens;
        }

        private string ParseWord(string code, int pos)
        {
            char liter;
            StringBuilder buffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsLetterOrDigit(liter) && liter != ':')
                    break;

                buffer.Append(liter);
                pos++;
            }

            return buffer.ToString();
        }

        private string ParseNumber(string code, int pos)
        {
            char liter;
            StringBuilder buffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsLetterOrDigit(liter) && liter != '.')
                    break;

                buffer.Append(liter);
                pos++;
            }

            return buffer.ToString();
        }

        private string ParseString(string code, int pos)
        {
            char liter;
            StringBuilder buffer = new StringBuilder();
            int quotesCount = 0;
            while (pos < code.Length)
            {
                liter = code[pos];
                if(liter == '\"')
                    quotesCount++;
                else if(quotesCount == 2 || liter == '\n')
                    break;

                buffer.Append(liter);

                pos++;
            }

            return buffer.ToString();
        }

        private string ParseOperator(string code, int pos)
        {
            string liter = code[pos].ToString();

            string firstCharacter = "<>=";
            string secondCharacter = "=";
            if (pos < code.Length - 1)
                if (firstCharacter.Contains(liter) && secondCharacter.Contains(code[pos + 1]))
                    liter += code[pos + 1];

            return liter;
        }
    }
}

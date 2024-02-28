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

            code = code.Replace("\n", "").Replace("\t", "").Replace("\r", "");

            string rawToken;
            do
            {
                char liter = code[position];

                if (char.IsWhiteSpace(liter))
                {
                    tokens.Add(new Token(liter.ToString(), position));
                    position += 1;
                }
                else if (TryParseWord(code, position, out rawToken)
                    || TryParseNumber(code, position, out rawToken)
                    || TryParseOperator(code, position, out rawToken)
                    || TryParseString(code, position, out rawToken))
                {
                    tokens.Add(new Token(rawToken, position));
                    position += rawToken.Length;
                }
                else
                {
                    position += 1;
                }

            } while (position < code.Length);

            return tokens;
        }

        private bool TryParseWord(string code, int pos, out string result)
        {
            result = "error";
            
            char liter = code[pos];
            if (!char.IsLetter(liter)) return false;

            StringBuilder buffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsLetter(liter) && liter != ':')
                {
                    result = buffer.ToString();
                    return true;
                }

                buffer.Append(liter);
                pos++;
            }

            if (pos == code.Length)
            {
                result = buffer.ToString();
                return true;
            }

            return false;
        }

        private bool TryParseNumber(string code, int pos, out string result)
        {
            result = "error";
            char liter = code[pos];
            if (!char.IsDigit(liter)) return false;

            StringBuilder buffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsDigit(liter) && liter != '.')
                {
                    result = buffer.ToString();
                    return true;
                }

                buffer.Append(liter);
                pos++;
            }

            if (pos == code.Length)
            {
                result = buffer.ToString();
                return true;
            }

            return false;
        }

        private static bool TryParseString(string code, int pos, out string result)
        {
            result = "error";
            char liter = code[pos];
            if (liter != '\"') return false;

            StringBuilder strBuffer = new StringBuilder();
            int quotesCount = 0;
            while (pos < code.Length)
            {
                liter = code[pos];
                if(liter == '\"')
                    quotesCount++;
                else if(quotesCount == 2)
                    break;

                strBuffer.Append(liter);

                pos++;
            }

            if(quotesCount == 2)
            {
                result = strBuffer.ToString();
                return true;
            }

            return false;
        }

        private bool TryParseOperator(string code, int pos, out string result)
        {
            result = "error";
            string liter = code[pos].ToString();

            string symb = "<>=";
            if (pos < code.Length - 1)
                if (symb.Contains(liter) && symb.Contains(code[pos + 1]))
                    liter += code[pos + 1];

            if (!Token.DefaultTokenExists(liter))
                return false;

            result = liter;
            return true;
        }
    }
}

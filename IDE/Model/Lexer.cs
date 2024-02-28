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

            do
            {
                char liter = code[position];
                if (liter == '\0')
                    break;

                if (char.IsWhiteSpace(liter))
                {
                    tokens.Add(new Token(liter.ToString(), position));
                    position += 1;
                }

                string word;
                if (TryParseWord(code, ref position, out word)
                    || TryParseNumber(code, ref position, out word)
                    || TryParseOperator(code, ref position, out word)
                    || TryParseString(code, ref position, out word))
                {
                    tokens.Add(new Token(word, position));
                    continue;
                }

                position += 1;

            } while (position < code.Length);

            return tokens;
        }

        private bool TryParseWord(string code, ref int pos, out string word)
        {
            word = "error";
            if(pos >= code.Length) 
                return false;
            char liter = code[pos];
            if (!char.IsLetter(liter))
                return false;

            StringBuilder wordBuffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsLetter(liter) && liter != ':')
                {
                    word = wordBuffer.ToString();
                    return true;
                }

                wordBuffer.Append(liter);
                pos++;
            }

            return false;
        }

        private bool TryParseNumber(string code, ref int pos, out string number)
        {
            number = "error";
            if (pos >= code.Length)
                return false;
            char liter = code[pos];
            if (!char.IsDigit(liter))
                return false;

            StringBuilder numberBuffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsDigit(liter) && liter != '.')
                {
                    number = numberBuffer.ToString();
                    return true;
                }

                numberBuffer.Append(liter);
                pos++;
            }

            return false;
        }

        private static bool TryParseString(string code, ref int pos, out string str)
        {
            str = "error";
            if (pos >= code.Length)
                return false;
            char literal = code[pos];
            if (literal != '\"')
                return false;

            StringBuilder strBuffer = new StringBuilder();
            int quotesCount = 0;
            while (pos < code.Length)
            {
                literal = code[pos];
                if(literal == '\"')
                    quotesCount++;
                else if(quotesCount == 2)
                    break;

                strBuffer.Append(literal);

                pos++;
            }

            if(quotesCount == 2)
            {
                str = strBuffer.ToString();
                return true;
            }

            return false;
        }

        private bool TryParseOperator(string code, ref int pos, out string operation)
        {
            operation = "error";
            if (pos >= code.Length)
                return false;
            string liter = code[pos].ToString();

            string symb = "<>=";
            if (pos < code.Length - 1)
                if (symb.Contains(liter) && symb.Contains(code[pos + 1]))
                    liter += code[pos + 1];

            if (!Token.DefaultTokenExists(liter))
                return false;

            operation = liter;
            pos += operation.Length;
            return true;
        }
    }
}

using System.Collections.Generic;
using System.Text;

namespace IDE.Model
{
    internal class Lexer
    {
        public IEnumerable<Token> Scan(string code)
        {
            List<Token> tokens = new List<Token>();
            int position = 0;

            do
            {
                char liter = code[position];

                if (char.IsWhiteSpace(liter))
                {
                    tokens.Add(new Token(liter.ToString(), position));
                    position += 1;
                }

                string word;
                if (TryParseWord(code, position, out word)
                    || TryParseNumber(code, position, out word)
                    || TryParseSymbol(code, position, out word))
                {
                    tokens.Add(new Token(word, position));
                    position += word.Length;
                    continue;
                }

                position += 1;

            } while (position < code.Length);

            return tokens;
        }

        private bool TryParseWord(string code, int pos, out string word)
        {
            word = "error";
            char liter = code[pos];
            if (char.IsLetter(liter) == false)
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

        private bool TryParseNumber(string code, int pos, out string number)
        {
            number = "error";
            char liter = code[pos];
            if (!char.IsDigit(liter) && liter != '-')
                return false;

            StringBuilder numberBuffer = new StringBuilder();
            while (pos < code.Length)
            {
                liter = code[pos];
                if (!char.IsDigit(liter) && liter != '.' && liter != '-')
                {
                    number = numberBuffer.ToString();
                    return true;
                }

                numberBuffer.Append(liter);
                pos++;
            }

            return false;
        }

        private bool TryParseSymbol(string code, int pos, out string symbol)
        {
            symbol = "error";
            char liter = code[pos];

            switch (liter)
            {
                case '<':
                case '>':
                case '(':
                case ')':
                case ',':
                case ';':
                    symbol = liter.ToString();
                    return true;
            }

            return false;
        }
    }
}

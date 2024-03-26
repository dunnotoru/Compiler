using System.Text;

namespace IDE.Model.Parser.States
{
    internal class ComplexState : IParserState
    {
        public string Handle(Parser parser, string code, int position)
        {
            if (position >= code.Length)
            {
                parser.AddError(new ParseError(position, position, "incomplete line", ""));
                return code;
            }

            string whitespaceCharacters = " \n\r\t";
            while (whitespaceCharacters.Contains(code[position]))
            {
                code = code.Remove(position, 1);
            }

            string expected = "std::complex<double> ";

            StringBuilder errorBuffer = new StringBuilder();
            for (int expectedPos = 0; expectedPos < expected.Length; expectedPos++)
            {
                if (position >= code.Length)
                {
                    parser.AddError(new ParseError(position, position, "incomplete line", ""));
                    return code;
                }

                if (expected[expectedPos] != code[position])
                {
                    errorBuffer.Append(code[position]);
                    code = code.Remove(position, 1);
                    expectedPos--;
                }
                else
                {
                    if(errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "std::complex<double> ", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }

                    position++;
                }
            }

            parser.State = new IdentifierState();
            return parser.State.Handle(parser, code, position);
        }
    }
}

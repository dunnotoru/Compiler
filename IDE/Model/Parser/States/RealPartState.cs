using System.Text;

namespace IDE.Model.Parser.States
{
    internal class RealPartState : IParserState
    {
        public string Handle(Parser parser, string code, int position)
        {
            char symbol;
            StringBuilder errorBuffer = new StringBuilder();

            while (position < code.Length)
            {
                if (position >= code.Length)
                {
                    parser.AddError(new ParseError(position, position, "incomplete line", ""));
                    return code;
                }

                char c = code[position];
                if (!char.IsDigit(c) && c != '+' && c != '-')
                {
                    errorBuffer.Append(c);
                    code = code.Remove(position, 1);
                }
                else
                {
                    if (errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "real start", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }

                    position++;
                    break;
                }
                position++;
            }

            errorBuffer.Clear();
            while (position < code.Length)
            {
                if (position >= code.Length)
                {
                    parser.AddError(new ParseError(position, position, "incomplete line", ""));
                    return code;
                }

                symbol = code[position];
                if (!char.IsDigit(symbol) && symbol != '.')
                {
                    errorBuffer.Append(symbol);
                    code = code.Remove(position, 1);
                }
                else if(symbol == '.')
                {
                    if (errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "real number", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }

                    position++;
                    break;
                }
                else
                {
                    if(errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "real number", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }
                }

                position++;
            }

            errorBuffer.Clear();
            while(position < code.Length)
            {
                symbol = code[position];

                if (!char.IsDigit(symbol) && symbol != ',')
                {
                    errorBuffer.Append(symbol);
                    code = code.Remove(position, 1);
                }
                else if(symbol == ',')
                {
                    if (errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "real number", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }

                    position++;
                    break;
                }
                else
                {
                    if (errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "real number", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }
                }

                position++;
            }

            parser.State = new ImaginaryPartState();
            return parser.State.Handle(parser, code, position);
        }
    }
}

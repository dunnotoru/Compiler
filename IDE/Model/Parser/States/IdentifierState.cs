using System.Text;

namespace IDE.Model.Parser.States
{
    internal class IdentifierState : IParserState
    {
        public string Handle(Parser parser, string code, int position)
        {
            char c;

            StringBuilder errorBuffer = new StringBuilder();
            while (position < code.Length)
            {
                if (position >= code.Length)
                {
                    parser.AddError(new ParseError(position, position, "incomplete line", ""));
                    return code;
                }

                c = code[position];
                if(!char.IsLetter(c) && c != '_')
                {
                    errorBuffer.Append(c);
                    code = code.Remove(position, 1);
                }
                else
                {
                    if(errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "identifier start", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }

                    break;
                }
            }

            errorBuffer.Clear();
            while (position < code.Length)
            {
                c = code[position];

                if(c == '(')
                {
                    position++;
                    break;
                }

                if(!char.IsLetter(c) && !char.IsDigit(c) && c != '_')
                {
                    errorBuffer.Append(c);
                    code = code.Remove(position, 1);
                }
                else
                {
                    if (errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "identifier", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }
                    position++;
                }
            }

            if (errorBuffer.Length > 0)
            {
                parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "identifier", errorBuffer.ToString()));
                errorBuffer.Clear();
            }

            parser.State = new RealPartState();
            return parser.State.Handle(parser, code, position);
        }
    }
}

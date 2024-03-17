using System.Runtime.Versioning;
using System.Text;

namespace IDE.Model.Parser.States
{
    internal class EndState : IParserState
    {
        public void Handle(Parser parser, string code, int position)
        {
            StringBuilder errorBuffer = new StringBuilder();
            while (position < code.Length)
            {
                if (position >= code.Length)
                {
                    parser.AddError(new ParseError(position, position, "incomplete line", ""));
                    return;
                }
                char c = code[position];
                if (c != ';')
                {
                    errorBuffer.Append(c);
                    code.Remove(position);
                }
                else
                {
                    if (errorBuffer.Length > 0)
                    {
                        parser.AddError(new ParseError(position + 1, position + errorBuffer.Length, "semicolon", errorBuffer.ToString()));
                        errorBuffer.Clear();
                    }

                    break;
                }
                position++;
            }

            position++;

            if(position == code.Length)
            {
                return;
            }

            parser.State = new ComplexState();
            parser.State.Handle(parser, code, position);
        }
    }
}



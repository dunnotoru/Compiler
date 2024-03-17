using System.Text;

namespace IDE.Model.Parser.States
{
    internal class RealPartState : IParserState
    {
        public void Handle(Parser parser, string code, int position)
        {
            char symbol;
            StringBuilder errorBuffer = new StringBuilder();
            while (position < code.Length)
            {
                symbol = code[position];

                if (!char.IsDigit(symbol) && symbol != '.')
                {
                    errorBuffer.Append(symbol);
                    code.Remove(position);
                }
                else if(symbol == '.')
                {
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
                    code.Remove(position);
                }
                else if(symbol == ',')
                {
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
            parser.State.Handle(parser, code, position);
        }
    }
}

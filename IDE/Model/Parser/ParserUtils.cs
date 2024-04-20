using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDE.Model.Parser
{
    internal static class ParserUtils
    {
        /// <summary>
        /// Returns false if tokens size was 0 or became 0 after trim;
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public static bool TrimWhitespaceTokens(ref List<Token> tokens)
        {
            if (tokens.Count == 0) return false;
            foreach (Token token in tokens.ToList())
            {
                if (string.IsNullOrWhiteSpace(token.RawToken))
                {
                    tokens.Remove(token);
                }
                else
                {
                    break;
                }
            }
            
            if (tokens.Count == 0 ) return false;

            return true;
        }

        public static string ComposeExpectedString(List<Token> errors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Token token in errors)
            {
                sb.Append(token.RawToken);
            }
            return sb.ToString();
        }

        public static void CreateErrorFromBuffer(Parser parser, List<Token> errorBuffer, string expected)
        {
            if (errorBuffer.Count > 0)
            {
                ParseError error = new ParseError(errorBuffer.First().StartPos, ComposeExpectedString(errorBuffer), expected);
                parser.AddError(error);
            }
        }

        public static void CreateError(Parser parser, int pos, string expected)
        {
            ParseError error = new ParseError(pos, "", expected);
            parser.AddError(error);
        }
    }
}

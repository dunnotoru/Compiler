using IDE.Model;
using IDE.Model.Parser;
using IDE.Services.Abstractions;
using System.Collections.Generic;

namespace IDE.Services
{
    internal class ParseService : IParseService
    {
        private Parser _parser = new Parser();

        public List<ParseError> Parse(List<Token> tokens)
        {
            return _parser.Parse(tokens);
        }
    }
}

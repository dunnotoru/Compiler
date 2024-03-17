using IDE.Model.Parser;
using System.Collections.Generic;

namespace IDE.Services.Abstractions
{
    internal interface IParseService
    {
        List<ParseError> Parse(string code);
    }
}

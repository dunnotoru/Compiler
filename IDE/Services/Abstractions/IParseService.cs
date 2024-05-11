using IDE.Model;
using IDE.Model.Parser;
using System.Collections.Generic;

namespace IDE.Services.Abstractions
{
    internal interface IParseService
    {
        (List<ParseError>, List<Token>) Parse(List<Token> tokens);
    }
}

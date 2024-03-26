using IDE.Model.Parser;
using System.Collections.Generic;

namespace IDE.Services.Abstractions
{
    internal interface IParseService
    {
        (List<ParseError>, string) Parse(string code);
    }
}

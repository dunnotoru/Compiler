using IDE.Model;
using System.Collections.Generic;

namespace IDE.Services.Abstractions
{
    internal interface IScanService
    {
        IEnumerable<Token> Scan(string code);
    }
}

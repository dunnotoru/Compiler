using IDE.Model;
using System.Collections.Generic;

namespace IDE.Services.Abstractions
{
    internal interface ITetradService
    {
        List<Tetrad> GetTetrads(List<Token> token);
    }
}

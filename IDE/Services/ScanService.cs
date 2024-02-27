using IDE.Model;
using IDE.Services.Abstractions;
using System.Collections.Generic;

namespace IDE.Services
{
    internal class ScanService : IScanService
    {
        private Lexer _lexer;
        public ScanService()
        {
            _lexer = new Lexer();
        }

        public IEnumerable<Token> Scan(string code)
        {
            return _lexer.Scan(code);
        }
    }
}

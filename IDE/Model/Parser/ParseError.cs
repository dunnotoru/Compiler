namespace IDE.Model.Parser
{
    internal class ParseError
    {
        private Token _token;
        public ParseError(Token token)
        {
            _token = token;
        }

        public string GetMessage()
        {
            return $"Ожидалось {_token.RawToken} на позиции {_token.StartPos} - {_token.EndPos}";
        }
    }
}

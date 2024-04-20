using IDE.Model;

namespace IDE.ViewModel
{
    internal class TokenViewModel : ViewModelBase
    {
        private string _code;
        private string _rawToken;
        private string _tokenType;
        private string _startIndex;
        private string _endIndex;

        public TokenViewModel(Token token)
        {
            _code = ((int)token.Type).ToString();
            _rawToken = token.RawToken;
            _tokenType = token.Type.ToString();
            _startIndex = token.StartPos.ToString();
            _endIndex = token.EndPos.ToString();
        }

        public string EndIndex
        {
            get { return _endIndex; }
            set { _endIndex = value; OnPropertyChanged(); }
        }
        public string StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; OnPropertyChanged(); }
        }

        public string TokenType
        {
            get { return _tokenType; }
            set { _tokenType = value; OnPropertyChanged(); }
        }
        public string RawToken
        {
            get { return _rawToken; }
            set { _rawToken = value; OnPropertyChanged(); }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(); }
        }
    }
}

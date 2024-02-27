namespace IDE.ViewModel
{
    internal class TokenViewModek : ViewModelBase
    {
		private string _code;
		private string _rawToken;
		private string _tokenType;
		private string _startIndex;
		private string _endIndex;

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
			set { _code = value; }
		}
	}
}

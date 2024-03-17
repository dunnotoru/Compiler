using IDE.Model.Parser;

namespace IDE.ViewModel
{
    internal class ParseErrorViewModel : ViewModelBase
    {
        private string _expectedToken;
        private string _discardedFragment;
        private string _startPos;
        private string _endPos;

        public ParseErrorViewModel(ParseError error)
        {
            _expectedToken = error.ExpectedTokenType.ToString();
            _startPos = error.StartPos.ToString();
            _endPos = error.EndPos.ToString();
            _discardedFragment = error.DiscardedFragment;
        }

        public string EndPos
        {
            get { return _endPos; }
            set { _endPos = value; OnPropertyChanged(); }
        }

        public string StartPos
        {
            get { return _startPos; }
            set { _startPos = value; OnPropertyChanged(); }
        }

        public string DiscardedFragment
        {
            get { return _discardedFragment; }
            set { _discardedFragment = value; OnPropertyChanged(); }
        }

        public string ExpectedToken
        {
            get { return _expectedToken; }
            set { _expectedToken = value; OnPropertyChanged(); }
        }
    }
}
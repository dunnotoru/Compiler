using IDE.Model.Parser;

namespace IDE.ViewModel
{
    internal class ParseErrorViewModel : ViewModelBase
    {
        private string _actual;
        private string _expected;
        private string _pos;

        public ParseErrorViewModel(ParseError error)
        {
            _actual = error.Actual.ToString();
            _pos = error.Pos.ToString();
            _expected = error.Expected;
        }

        public string Pos
        {
            get { return _pos; }
            set { _pos = value; OnPropertyChanged(); }
        }

        public string Expected
        {
            get { return _expected; }
            set { _expected = value; OnPropertyChanged(); }
        }

        public string Actual
        {
            get { return _actual; }
            set { _actual = value; OnPropertyChanged(); }
        }
    }
}
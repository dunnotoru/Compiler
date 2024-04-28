namespace IDE.ViewModel
{
    internal class RegexMatchViewModel : ViewModelBase
    {
        public RegexMatchViewModel(string matchString, int position)
        {
            MatchString = matchString;
            Position = position;
        }

        public string MatchString { get; private set; }
        public int Position { get; private set; }
    }
}

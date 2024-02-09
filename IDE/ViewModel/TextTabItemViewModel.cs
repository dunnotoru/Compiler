namespace IDE.ViewModel
{
    internal class TextTabItemViewModel : ViewModelBase
    {
		private string _header;
		private string _content;
		private string _path;

        public TextTabItemViewModel()
        {
            Header = "unnamed";
            Content = "content";
            Path = string.Empty;
        }

        public string Content
		{
			get { return _content; }
			set { _content = value; OnPropertyChanged(); }
		}

		public string Header
		{
			get { return _header; }
			set { _header = value; OnPropertyChanged(); }
		}

        public string Path
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged(); }
        }
    }
}

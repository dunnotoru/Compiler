using System.IO;

namespace IDE.ViewModel
{
    internal class TextTabItemViewModel : ViewModelBase
    {
		private string _content;
		private string _fileName;
        private bool _isNew;

        public TextTabItemViewModel()
        {
            Content = string.Empty;
            FileName = string.Empty;
        }

        public string Content
		{
			get { return _content; }
			set { _content = value; OnPropertyChanged(); }
		}

        public bool IsNew
        {
            get { return _isNew; }
            set { _isNew = value; OnPropertyChanged(); }
        }

        public string Header => Path.GetFileName(FileName);

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(); }
        }
    }
}

using System;
using System.IO;
using System.Windows.Input;

namespace IDE.ViewModel
{

    internal class TabItemViewModel : ViewModelBase
    {
		private string _content;
		private string _fileName;
        private bool _isUnsaved;

        public event EventHandler Close;

        public ICommand CloseCommand => new RelayCommand(ExecuteClose);

        public TabItemViewModel(string fileName)
        {
            FileName = fileName;
            Content = string.Empty;
        }

        public TabItemViewModel(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }

        private void ExecuteClose(object obj)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        public string Content
		{
			get { return _content; }
			set { _content = value; OnPropertyChanged(); IsUnsaved = true; }
		}

        public bool IsUnsaved
        {
            get { return _isUnsaved; }
            set { _isUnsaved = value; OnPropertyChanged(); }
        }

        public string Header => Path.GetFileName(FileName);

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(); }
        }
    }
}

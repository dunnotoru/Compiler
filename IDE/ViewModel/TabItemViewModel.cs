using System;
using System.IO;
using System.Windows.Input;

namespace IDE.ViewModel
{

    internal class TabItemViewModel : ViewModelBase
    {
		private string _content;
		private string _fileName;
        private bool _isChanged;

        public event EventHandler Close;

        public ICommand CloseCommand { get; }

        public TabItemViewModel()
        {
            Content = string.Empty;
            FileName = string.Empty;

            CloseCommand = new RelayCommand(ExecuteClose);
        }

        private void ExecuteClose(object obj)
        {
            Close?.Invoke(this, EventArgs.Empty);
        }

        public string Content
		{
			get { return _content; }
			set { _content = value; OnPropertyChanged(); }
		}

        public bool IsChanged
        {
            get { return _isChanged; }
            set { _isChanged = value; OnPropertyChanged(); }
        }

        public string Header => Path.GetFileName(FileName);

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(); }
        }
    }
}

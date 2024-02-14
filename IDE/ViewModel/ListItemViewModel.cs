using System;

namespace IDE.ViewModel
{
    class ListItemViewModel : ViewModelBase
    {
        private string _content;
        private ViewModelBase _viewModel;

        public ListItemViewModel(string content, Func<ViewModelBase> getViewModel)
        {
            Content = content;
            ViewModel = getViewModel();
        }

        public ViewModelBase ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; OnPropertyChanged(); }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }
    }
}

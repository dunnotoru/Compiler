using IDE.Model.Abstractions;
using IDE.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace IDE.ViewModel
{
    internal class ShellWindowViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        private IFileService _fileService;

		private ObservableCollection<TextTabItemViewModel> _tabs;
        private TextTabItemViewModel _selectedTab;
        public RelayCommand CreateCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand OpenCommand { get; }

        public ShellWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            Tabs = new ObservableCollection<TextTabItemViewModel>();
            _dialogService = dialogService;
            _fileService = fileService;

            CreateCommand = new RelayCommand(Create);
            SaveCommand = new RelayCommand(Save, _ => SelectedTab != null);
            OpenCommand = new RelayCommand(Open);
        }

        private void Open(object obj)
        {
            string fileName = _dialogService.OpenFileDialog();
            TextTabItemViewModel tab = new TextTabItemViewModel();
            tab.Path = fileName;
            tab.Content = _fileService.LoadFile(fileName);
            _tabs.Add(tab);
            SelectedTab = tab;
        }

        private void Save(object obj)
        {
            if (SelectedTab == null) return;
            if (SelectedTab.Path == string.Empty)
            {
                SaveAs(obj);
                return;
            }

            _fileService.SaveFile(SelectedTab.Path, SelectedTab.Content);
        }

        private void SaveAs(object obj)
        {
            if (SelectedTab == null) return;

            SelectedTab.Path = _dialogService.SaveAsFileDialog(SelectedTab.Content);
        }

        private void Create(object obj)
        {
            TextTabItemViewModel tab = new TextTabItemViewModel();
            Tabs.Add(tab);
            SelectedTab = tab;
        }

        public TextTabItemViewModel SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TextTabItemViewModel> Tabs
		{
			get { return _tabs; }
			set { _tabs = value; OnPropertyChanged(); }
		}
	}
}

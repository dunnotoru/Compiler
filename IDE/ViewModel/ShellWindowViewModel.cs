using IDE.Model.Abstractions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace IDE.ViewModel
{
    internal class ShellWindowViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        private IFileService _fileService;

		private ObservableCollection<TabItemViewModel> _tabs;
        private TabItemViewModel _selectedTab;
        public ICommand CreateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }

        public ShellWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            Tabs = new ObservableCollection<TabItemViewModel>();
            _dialogService = dialogService;
            _fileService = fileService;

            CreateCommand = new RelayCommand(Create);
            SaveCommand = new RelayCommand(Save, _ => SelectedTab != null);
            SaveCommand = new RelayCommand(SaveAs, _ => SelectedTab != null);
            OpenCommand = new RelayCommand(Open);
        }

        private void Open(object obj)
        {
            string fileName = _dialogService.OpenFileDialog();
            if (string.IsNullOrWhiteSpace(fileName)) return;
            
            string content = _fileService.LoadFile(fileName);

            TabItemViewModel tab = new TabItemViewModel();
            tab.FileName = fileName;
            tab.Content = content;
            AddTab(tab);
        }

        private void Save(object obj)
        {
            if (SelectedTab == null) return;
            if (SelectedTab.FileName == string.Empty)
            {
                SaveAs(obj);
                return;
            }

            _fileService.SaveFile(SelectedTab.FileName, SelectedTab.Content);
        }

        private void SaveAs(object obj)
        {
            if (SelectedTab == null) return;
            string fileName = _dialogService.SaveAsFileDialog();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            SelectedTab.FileName = fileName;
            _fileService.SaveFile(SelectedTab.FileName, SelectedTab.Content);
        }

        private void Create(object obj)
        {
            TabItemViewModel tab = new TabItemViewModel();
            AddTab(tab);
        }

        private void AddTab(TabItemViewModel tab)
        {
            Tabs.Add(tab);
            SelectedTab = tab;

            tab.Close += RemoveTab;
        }

        private void RemoveTab(object? sender, System.EventArgs e)
        {
            if (sender is not TabItemViewModel) return;

            TabItemViewModel tab = (TabItemViewModel)sender;

            tab.Close -= RemoveTab;
            Tabs.Remove(tab);
        }

        public TabItemViewModel SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TabItemViewModel> Tabs
		{
			get { return _tabs; }
			set { _tabs = value; OnPropertyChanged(); }
		}
	}
}

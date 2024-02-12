using IDE.Model.Abstractions;
using IDE.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace IDE.ViewModel
{
    internal class ShellWindowViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        private IFileService _fileService;

		private ObservableCollection<TextTabItemViewModel> _tabs;
        private TextTabItemViewModel _selectedTab;
        public ICommand CreateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }

        public ShellWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            Tabs = new ObservableCollection<TextTabItemViewModel>();
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

            TextTabItemViewModel tab = new TextTabItemViewModel();
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
            TextTabItemViewModel tab = new TextTabItemViewModel();
            AddTab(tab);
        }

        private void AddTab(TextTabItemViewModel tab)
        {
            Tabs.Add(tab);
            SelectedTab = tab;

            tab.Close += RemoveTab;
        }

        private void RemoveTab(object? sender, System.EventArgs e)
        {
            if (sender is not TextTabItemViewModel) return;

            TextTabItemViewModel tab = (TextTabItemViewModel)sender;

            tab.Close -= RemoveTab;
            Tabs.Remove(tab);
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

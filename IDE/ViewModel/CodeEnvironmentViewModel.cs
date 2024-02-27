using IDE.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace IDE.ViewModel
{
    internal sealed class CodeEnvironmentViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IFileService _fileService;
        private readonly ICloseService _closeService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly ILogger _logger;
        private readonly ILocalizationProvider _localization; 
        private readonly NavigationService _navigationService; 

		private ObservableCollection<TabItemViewModel> _tabs;
        private TabItemViewModel? _selectedTab;

        public ICommand CreateCommand => new RelayCommand(Create);
        public ICommand SaveCommand => new RelayCommand(Save, _ => SelectedTab != null);
        public ICommand SaveAsCommand => new RelayCommand(SaveAs, _ => SelectedTab != null);
        public ICommand OpenCommand => new RelayCommand(Open);
        public ICommand CloseCommand => new RelayCommand(Close);
        public ICommand NavigateToSettingsCommand => new RelayCommand(NavigateToSettings);
        public ICommand ShowHelpCommand => new RelayCommand(ShowHelp);
        public ICommand ShowAboutCommand => new RelayCommand(ShowAbout);

        public ICommand ScanCommand => new RelayCommand(Scan);

        private void Scan(object? obj)
        {
            throw new NotImplementedException();
        }

        public CodeEnvironmentViewModel(IDialogService dialogService,
                                    IFileService fileService,
                                    ICloseService closeService,
                                    IMessageBoxService messageBoxService,
                                    ILogger logger,
                                    ILocalizationProvider localization,
                                    NavigationService navigationService)
        {
            _tabs = new ObservableCollection<TabItemViewModel>();
            _dialogService = dialogService;
            _fileService = fileService;
            _closeService = closeService;
            _messageBoxService = messageBoxService;
            _logger = logger;
            _localization = localization;
            _navigationService = navigationService;
        }

        private void Open(object? obj)
        {
            string fileName = _dialogService.OpenFileDialog();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            TabItemViewModel? tab = Tabs.FirstOrDefault(_ => _.FileName.ToLower().Equals(fileName.ToLower()));
            if (tab is not null)
            {
                SelectedTab = tab;
                return;
            }
            
            string content = _fileService.LoadFile(fileName);

            tab = new TabItemViewModel(fileName, content);
            tab.IsUnsaved = false;

            AddTab(tab);
            _logger.LogDebug("File opened " + fileName);
        }

        private void Save(object? obj)
        {
            if (SelectedTab is null) return;

            _fileService.SaveFile(SelectedTab.FileName, SelectedTab.Content);

            SelectedTab.IsUnsaved = false;
            _logger.LogDebug("File saved " + SelectedTab.FileName);
        }

        private void SaveAs(object? obj)
        {
            if (SelectedTab is null) return;
            string fileName = _dialogService.SaveAsFileDialog();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            SelectedTab.FileName = fileName;
            _fileService.SaveFile(SelectedTab.FileName, SelectedTab.Content);
            SelectedTab.IsUnsaved = false;

            _logger.LogDebug("File " + fileName + " saved as " + SelectedTab.FileName);
        }

        private void SaveAll()
        {
            foreach(TabItemViewModel tab in Tabs)
                Save(tab);
        }

        private void Create(object? obj)
        {
            string fileName = _dialogService.SaveAsFileDialog();
            if (string.IsNullOrEmpty(fileName)) return;

            TabItemViewModel tab = new TabItemViewModel(fileName);
            tab.IsUnsaved = true;

            AddTab(tab);
            _logger.LogDebug("File created " + fileName);
        }

        private void AddTab(TabItemViewModel tab)
        {
            Tabs.Add(tab);
            SelectedTab = tab;
            tab.Close += RemoveTab;
        }

        private void RemoveTab(object? sender, EventArgs e)
        {
            if (sender is not TabItemViewModel) return;

            TabItemViewModel tab = (TabItemViewModel)sender;
            MessageResult result = MessageResult.No;
            if (tab.IsUnsaved == true)
            {
                result = _messageBoxService.ShowYesNoCancel(_localization.GetLocalizedString("msg_save_changes"));
            }

            switch (result)
            {
                case MessageResult.Yes:
                    _fileService.SaveFile(tab.FileName, tab.Content);
                    break;

                case MessageResult.Cancel:
                    return;

                default:
                    break;
            }

            tab.Close -= RemoveTab;
            Tabs.Remove(tab);
        }

        private void Close(object? obj)
        {
            MessageResult result = MessageResult.No;
            if (Tabs.Any(_ => _.IsUnsaved == true))
                result = _messageBoxService.ShowYesNoCancel(_localization.GetLocalizedString("msg_save_changes"));

            switch (result)
            {
                case MessageResult.Yes:
                    SaveAll(); break;

                case MessageResult.Cancel:
                    return;

                default:
                    break;
            }

            _logger.LogDebug("Application closed");
            _closeService.Close();
        }

        private void NavigateToSettings(object? obj)
        {
            _navigationService.Navigate<SettingsViewModel>();
        }


        private void ShowAbout(object? obj)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"Resources\about.html")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        private void ShowHelp(object? obj)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"Resources\help.html")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public TabItemViewModel? SelectedTab
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

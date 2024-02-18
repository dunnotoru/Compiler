using IDE.Services.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace IDE.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        private ListItemViewModel? _selectedItem;
        private ObservableCollection<ListItemViewModel> _items;
        private readonly ILocalizationProvider _localizationProvider;
        private readonly NavigationService _navigationService;

        public ICommand BackCommand => new RelayCommand(Back);

        public SettingsViewModel(ILocalizationProvider localizationProvider, NavigationService navigationService)
        {
            _localizationProvider = localizationProvider;
            _navigationService = navigationService;
            _items = new ObservableCollection<ListItemViewModel>();
            Items.Add(new ListItemViewModel(_localizationProvider.GetLocalizedString("settings_language"), () => new LanguageSettingsViewModel()));
        }

        public ObservableCollection<ListItemViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public ListItemViewModel? SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private void Back(object? obj)
        {
            _navigationService.Navigate<CodeEnvironmentViewModel>();
        }
    }
}

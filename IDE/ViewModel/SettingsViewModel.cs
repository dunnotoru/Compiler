using IDE.Services.Abstractions;
using System.Collections.ObjectModel;

namespace IDE.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        private ListItemViewModel _selectedItem;
        private ObservableCollection<ListItemViewModel> _items;
        private readonly ILocalizationProvider _localizationProvider;

        public SettingsViewModel(ILocalizationProvider localizationProvider)
        {
            _localizationProvider = localizationProvider;
            Items = new ObservableCollection<ListItemViewModel>();
            Items.Add(new ListItemViewModel(_localizationProvider.GetLocalizedString("settings_language"), () => new LanguageSettingsViewModel()));
        }

        public ObservableCollection<ListItemViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public ListItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

    }
}

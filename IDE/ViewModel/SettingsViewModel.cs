using System.Collections.ObjectModel;

namespace IDE.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        private ListItemViewModel _selectedItem;
        private ObservableCollection<ListItemViewModel> _items;

        public SettingsViewModel()
        {
            Items = new ObservableCollection<ListItemViewModel>();
            Items.Add(new ListItemViewModel("Language", () => new LanguageSettingsViewModel()));
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

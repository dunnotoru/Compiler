using System.Collections.ObjectModel;

namespace IDE.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        private ViewModelBase _current;
        private ObservableCollection<ViewModelBase> _items;

        public SettingsViewModel()
        {
            Items = new ObservableCollection<ViewModelBase>();
            Items.Add(new ListItemViewModel("Language", () => new LanguageSettingsViewModel()));
        }

        public ObservableCollection<ViewModelBase> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public ViewModelBase Current
        {
            get { return _current; }
            set { _current = value; OnPropertyChanged(); }
        }
    }
}

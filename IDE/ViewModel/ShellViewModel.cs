using IDE.Services;

namespace IDE.ViewModel
{
    internal class ShellViewModel : ViewModelBase
    {
        private readonly NavigationStore _store;

        private ViewModelBase _current;
        public ViewModelBase Current
        {
            get => _store.Current;
        }

        public ShellViewModel(NavigationStore store)
        {
            _store = store;
        }
    }
}

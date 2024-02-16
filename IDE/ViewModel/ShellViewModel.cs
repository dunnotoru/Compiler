using IDE.Services;
using System;

namespace IDE.ViewModel
{
    internal class ShellViewModel : ViewModelBase
    {
        private readonly NavigationStore _store;

        public ViewModelBase Current
        {
            get => _store.Current;
        }

        public ShellViewModel(NavigationStore store)
        {
            _store = store;
            _store.ViewModelChanged += OnViewModelChanged;
        }

        private void OnViewModelChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Current));
        }
    }
}

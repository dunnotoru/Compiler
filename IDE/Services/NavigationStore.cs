using IDE.ViewModel;
using System;

namespace IDE.Services
{
    internal class NavigationStore
    {
        private ViewModelBase? _current;

        public ViewModelBase? Current
        {
            get { return _current; }
            set { _current = value; ViewModelChanged?.Invoke(this, EventArgs.Empty); }
        }

        public event EventHandler? ViewModelChanged;
    }
}

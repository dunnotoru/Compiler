using IDE.ViewModel;

namespace IDE.Services.Abstractions
{
    internal class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly IViewModelFactory _viewModelFactory;

        public NavigationService(NavigationStore store, IViewModelFactory viewModelFactory)
        {
            _navigationStore = store;
            _viewModelFactory = viewModelFactory;
        }

        public void Navigate<T>() where T : ViewModelBase
        {
            _navigationStore.Current = _viewModelFactory.CreateViewModel<T>();
        }
    }
}

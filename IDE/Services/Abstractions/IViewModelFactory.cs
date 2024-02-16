using IDE.ViewModel;

namespace IDE.Services.Abstractions
{
    internal interface IViewModelFactory
    {
        ViewModelBase CreateViewModel<T>() where T : ViewModelBase;
    }
}

using IDE.ViewModel;
using System;

namespace IDE.Services.Abstractions
{
    interface IWindowService
    {
        void Show<T>(Func<T> getViewModel) where T : ViewModelBase;
        void ShowDialog<T>(Func<T> getViewModel) where T : ViewModelBase;
    }
}

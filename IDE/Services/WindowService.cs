using IDE.Services.Abstractions;
using IDE.ViewModel;
using System;
using System.Windows;

namespace IDE.Services
{
    class WindowService : IWindowService
    {
        public void Show<T>(Func<T> getViewModel) where T : ViewModelBase
        {
            Window window = new Window();
            window.Content = getViewModel();
            window.Show();
        }

        public void ShowDialog<T>(Func<T> getViewModel) where T : ViewModelBase
        {
            Window window = new Window();
            window.Content = getViewModel();
            window.ShowDialog();
        }
    }
}

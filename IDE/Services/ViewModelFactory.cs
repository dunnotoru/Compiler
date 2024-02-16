using IDE.Services.Abstractions;
using IDE.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IDE.Services
{
    internal class ViewModelFactory : IViewModelFactory
    {
        private readonly IServiceProvider _provider;

        public ViewModelFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ViewModelBase CreateViewModel<T>() where T : ViewModelBase
        {
            return _provider.GetRequiredService<T>();
        }
    }
}

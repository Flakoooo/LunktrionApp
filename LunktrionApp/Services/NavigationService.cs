using LunktrionApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LunktrionApp.Services
{
    public class NavigationService
    {
        private readonly IServiceProvider _provider;

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public NavigationService()
        {
            _provider = null!;

            CurrentPage = new DeviceViewModel();
        }

        public ViewModelBase? CurrentPage { get; private set; }

        public event Action? CurrentPageChanged;

        public void Navigate<T>()
            where T : ViewModelBase
        {
            CurrentPage = _provider.GetRequiredService<T>();

            CurrentPageChanged?.Invoke();
        }
    }
}

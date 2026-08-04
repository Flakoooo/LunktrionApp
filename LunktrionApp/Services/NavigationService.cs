using LunktrionApp.Models;
using LunktrionApp.Models.Interfaces;
using LunktrionApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LunktrionApp.Services
{
    public class NavigationService : IAsyncInitializable
    {
        private readonly IServiceProvider _provider;

        public event EventHandler? CurrentViewModelChanged;

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;

            CurrentViewModel = _provider.GetRequiredService<LoadingViewModel>();
        }

        public NavigationService()
        {
            _provider = null!;

            CurrentViewModel = new DeviceViewModel();
        }

        private ViewModelBase? _currentViewModel;

        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                if (_currentViewModel == value) return;

                _currentViewModel = value;

                CurrentViewModelChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task InitializeAsync()
        {
            await NavigateAsync<DeviceViewModel, DeviceIdentity?>(null);
        }

        public async Task NavigateAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _provider.GetRequiredService<TViewModel>();

            if (viewModel is IAsyncInitializable initializable)
                await initializable.InitializeAsync();

            CurrentViewModel = viewModel;
        }

        public async Task NavigateAsync<TViewModel, TParameter>(TParameter parameter) where TViewModel : ViewModelBase
        {
            var viewModel = _provider.GetRequiredService<TViewModel>();

            if (viewModel is IAsyncInitializable<TParameter> initializable)
                await initializable.InitializeAsync(parameter);

            CurrentViewModel = viewModel;
        }
    }
}

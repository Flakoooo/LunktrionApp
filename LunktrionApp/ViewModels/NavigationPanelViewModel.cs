using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LunktrionApp.Models;
using LunktrionApp.Services;
using System;
using System.Threading.Tasks;

namespace LunktrionApp.ViewModels
{
    public partial class NavigationPanelViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;

        public NavigationPanelViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public NavigationPanelViewModel()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
                );
            }

            _navigationService = new NavigationService();
        }

        private async Task NavigateToCurrentDevice()
        {
            await _navigationService.NavigateAsync<DeviceViewModel, DeviceIdentity?>(null);
        }

        private async Task NavigateToAllDevices()
        {
            await _navigationService.NavigateAsync<DevicesListViewModel>();
        }

        [RelayCommand]
        public async Task NavigateToCurrentDeviceCommandAsync()
        {
            await NavigateToCurrentDevice();
        }

        [RelayCommand]
        public async Task NavigateToAllDevicesCommandAsync()
        {
            await NavigateToAllDevices();
        }
    }
}

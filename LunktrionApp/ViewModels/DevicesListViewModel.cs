using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LunktrionApp.Models;
using LunktrionApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LunktrionApp.ViewModels
{
    public partial class DevicesListViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;

        public ObservableCollection<DeviceIdentity> Devices { get; } = [];

        public DevicesListViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS", Manufacturer = "MSI" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux", Manufacturer = "MSI" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2", Manufacturer = "ASUS" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows", Manufacturer = "IPHONE" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS", Manufacturer = "ACER" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows" });
        }

        public DevicesListViewModel()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
                );
            }

            _navigationService = null!;

            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS", Manufacturer = "MSI" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux", Manufacturer = "MSI" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2", Manufacturer = "ASUS" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows", Manufacturer = "IPHONE" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS", Manufacturer = "ACER" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк", OSName = "Windows OS" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон унопочный", OSName = "Linux" });
            Devices.Add(new DeviceIdentity { DeviceName = "Крутой пк 2", OSName = "Windows OS 2" });
            Devices.Add(new DeviceIdentity { DeviceName = "Телефон телепатический", OSName = "Linux Windows" });
        }

        private async Task NavigateToDevice(DeviceIdentity device)
        {
            await _navigationService.NavigateAsync<DeviceViewModel, DeviceIdentity?>(device);
        }

        [RelayCommand]
        public async Task NavigateToDeviceCommandAsync(DeviceIdentity device)
        {
            await NavigateToDevice(device);
        }
    }
}

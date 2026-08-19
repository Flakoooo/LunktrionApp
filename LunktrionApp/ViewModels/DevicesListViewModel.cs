using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using LunktrionApp.Models.Entities;
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

            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк", OperatingSystemName: "Windows OS", DeviceManufacturer: "MSI"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон унопочный", OperatingSystemName: "Linux", DeviceManufacturer: "MSI"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк 2", OperatingSystemName: "Windows OS 2", DeviceManufacturer: "ASUS"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон телепатический", OperatingSystemName: "Linux Windows", DeviceManufacturer: "IPHONE"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк", OperatingSystemName: "Windows OS", DeviceManufacturer: "ACER"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон унопочный", OperatingSystemName: "Linux"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк 2", OperatingSystemName: "Windows OS 2"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон телепатический", OperatingSystemName: "Linux Windows"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк", OperatingSystemName: "Windows OS"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон унопочный", OperatingSystemName: "Linux"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк 2", OperatingSystemName: "Windows OS 2"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон телепатический", OperatingSystemName: "Linux Windows"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк", OperatingSystemName: "Windows OS"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон унопочный", OperatingSystemName: "Linux"));
            Devices.Add(new DeviceIdentity(DeviceName: "Крутой пк 2", OperatingSystemName: "Windows OS 2"));
            Devices.Add(new DeviceIdentity(DeviceName: "Телефон телепатический", OperatingSystemName: "Linux Windows"));
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

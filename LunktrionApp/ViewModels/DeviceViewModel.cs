using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LunktrionApp.Hubs;
using LunktrionApp.Models.DTO;
using LunktrionApp.Models.Entities;
using LunktrionApp.Models.Interfaces;
using LunktrionApp.Services;
using System;
using System.Threading.Tasks;

namespace LunktrionApp.ViewModels
{
    public partial class DeviceViewModel : ViewModelBase, IDisposable, IAsyncInitializable<DeviceIdentity?>
    {
        private readonly MainHub _mainHub;
        private readonly DeviceIdentityService _identityService;
        private readonly DeviceInfoService _infoService;
        private readonly CommandExecutorService _commandExecutorService;

        public DeviceIdentity? CurrentDevice { get; set; }

        public bool IsCurrentDevice { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsConnectedText))]
        [NotifyPropertyChangedFor(nameof(IsConnectedColor))]
        public partial bool IsConnected { get; set; }
        public string IsConnectedText => $"{(IsConnected ? "В" : "Не в")} сети";
        public string IsConnectedColor => IsConnected ? "#5FA866" : "#D95C4A";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeviceCPUSpecifications))]
        public partial DeviceCPUInfo? DeviceCPUInfo { get; set; }
        public string DeviceCPUSpecifications => $"Ядер/Потоков {DeviceCPUInfo?.NumberOfCores}/{DeviceCPUInfo?.NumberOfLogicalProcessors}, " +
            $"Текущая/Базовая частота {DeviceCPUInfo?.CurrentClockSpeed / 1000.0:F2}/{DeviceCPUInfo?.MaxClockSpeed / 1000.0:F2} GHz";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeviceGPUSpecifications))]
        public partial DeviceGPUInfo? DeviceGPUInfo { get; set; }
        public string DeviceGPUSpecifications => $"Объем {DeviceGPUInfo?.VideoRAM / 1024.0 / 1024.0} MB, {DeviceGPUInfo?.MaxRefreshRate}";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeviceRAMSpecifications))]
        public partial DeviceRAMInfo? DeviceRAMInfo { get; set; }
        public string DeviceRAMSpecifications => $"Тип {DeviceRAMInfo?.Type}, Объем {DeviceRAMInfo?.Size / 1024.0 / 1024.0 / 1024.0} " +
            $"({DeviceRAMInfo?.AvailableSize / 1024.0 / 1024.0 / 1024.0:F2}) GB, Частота {DeviceRAMInfo?.Speed} MHz";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeviceDriveSpecifications))]
        public partial DeviceDriveInfo? DeviceDriveInfo { get; set; }
        public string DeviceDriveSpecifications => $"Объем/Доступно {DeviceDriveInfo?.TotalSize / 1024.0 / 1024.0 / 1024.0:F2}/{DeviceDriveInfo?.AvailableSize / 1024.0 / 1024.0 / 1024.0:F2} GB, Дисков {DeviceDriveInfo?.DriversCount}";

        public async Task InitializeAsync(DeviceIdentity? device = null)
        {
            var currentDevice = await _identityService.GetCurrentDeviceAsync();
            if (device is null)
            {
                CurrentDevice = currentDevice;
                IsCurrentDevice = true;

                DeviceCPUInfo = await _infoService.GetDeviceCPUInfoAsync();
                DeviceGPUInfo = await _infoService.GetDeviceGPUInfoAsync();
                DeviceRAMInfo = await _infoService.GetDeviceRAMInfoAsync();
                DeviceDriveInfo = await _infoService.GetDeviceDriveInfoAsync();
            }
            else
            {
                CurrentDevice = device;
                IsCurrentDevice = CurrentDevice.DeviceId == currentDevice.DeviceId;

                await _mainHub.RequestDeviceInfoAsync(device.DeviceId, currentDevice.DeviceId);
            }
        }

        public DeviceViewModel(
            MainHub mainHub,
            DeviceIdentityService identityService, 
            DeviceInfoService infoService,
            CommandExecutorService commandExecutorService
        )
        {
            _mainHub = mainHub;
            _identityService = identityService;
            _infoService = infoService;
            _commandExecutorService = commandExecutorService;

            IsConnected = _mainHub.IsConnected;

            _mainHub.ConnectionStatusChanged += OnConnectionStatusChanged;
            _mainHub.OnDeviceInfoReceived += OnDeviceInfoReceived;
        }

        public DeviceViewModel()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
                );
            }

            _mainHub = null!;
            _identityService = new DeviceIdentityService();
            _infoService = new DeviceInfoService();
            _commandExecutorService = null!;

            _ = InitializeAsync();
        }

        [RelayCommand]
        public async Task ExecuteDeviceCommandCommandAsync()
        {
            
        }

        private void OnConnectionStatusChanged(bool isConnected)
        {
            Dispatcher.UIThread.Post(() =>
            {
                IsConnected = isConnected;
            });
        }

        private void OnDeviceInfoReceived(DeviceInfoDTO deviceInfo)
        {
            Dispatcher.UIThread.Post(() =>
            {
                DeviceCPUInfo = deviceInfo.CPUInfo;
                DeviceGPUInfo = deviceInfo.GPUInfo;
                DeviceRAMInfo = deviceInfo.RAMInfo;
                DeviceDriveInfo = deviceInfo.DriveInfo;
            });
        }

        public void Dispose()
        {
            _mainHub.ConnectionStatusChanged -= OnConnectionStatusChanged;
            _mainHub.OnDeviceInfoReceived -= OnDeviceInfoReceived;
        }
    }
}

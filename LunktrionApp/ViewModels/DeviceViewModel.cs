using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using LunktrionApp.Models;
using LunktrionApp.Models.Interfaces;
using LunktrionApp.Services;
using System;
using System.Threading.Tasks;

namespace LunktrionApp.ViewModels
{
    public partial class DeviceViewModel : ViewModelBase, IAsyncInitializable<DeviceIdentity?>
    {
        private readonly DeviceIdentityService _identityService;
        private readonly DeviceInfoService _infoService;

        public DeviceIdentity? CurrentDevice { get; set; }

        public bool IsCurrentDevice { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeviceCPUSpecifications))]
        public partial DeviceCPUInfo? DeviceCPUInfo { get; set; }
        public string DeviceCPUSpecifications => $"Ядер/Потоков {DeviceCPUInfo?.NumberOfCores}/{DeviceCPUInfo?.NumberOfLogicalProcessors}, " +
            $"Текущая/Базовая частота {DeviceCPUInfo?.CurrentClockSpeed / 1000.0:F2}/{DeviceCPUInfo?.MaxClockSpeed / 1000.0:F2} GHz";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DeviceGPUSpecifications))]
        public partial DeviceGPUInfo? DeviceGPUInfo { get; set; }
        public string DeviceGPUSpecifications => $"Объем {DeviceGPUInfo?.VideoRAM / 1024.0 / 1024.0} MB";

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
            if (device is null)
            {
                CurrentDevice = await _identityService.GetCurrentDeviceAsync(true);
                IsCurrentDevice = true;
            }
            else
            {
                CurrentDevice = device;
                IsCurrentDevice = CurrentDevice.Id == (await _identityService.GetCurrentDeviceAsync(true)).Id;
            }

            DeviceCPUInfo = await _infoService.GetDeviceCPUInfoAsync(true);
            DeviceGPUInfo = await _infoService.GetDeviceGPUInfoAsync(true);
            DeviceRAMInfo = await _infoService.GetDeviceRAMInfoAsync(true);
            DeviceDriveInfo = await _infoService.GetDeviceDriveInfoAsync(true);
        }

        public DeviceViewModel(
            DeviceIdentityService identityService, 
            DeviceInfoService infoService
        )
        {
            _identityService = identityService;
            _infoService = infoService;
        }

        public DeviceViewModel()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
                );
            }

            _identityService = new DeviceIdentityService();
            _infoService = new DeviceInfoService();

            _ = InitializeAsync();
        }
    }
}

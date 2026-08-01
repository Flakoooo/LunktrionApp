using Avalonia.Controls;
using Hardware.Info;
using LunktrionApp.Models;
using LunktrionApp.Services;
using System;

namespace LunktrionApp.ViewModels
{
    public class DeviceViewModel : ViewModelBase
    {
        private readonly DeviceIdentityService _identityService;
        private readonly DeviceInfoService _infoService;

        public DeviceIdentity CurrentDevice { get; set; }

        public bool IsCurrentDevice => CurrentDevice.Id == _identityService.CurrentDevice.Id;

        public DeviceCPUInfo DeviceCPUInfo { get; set; }
        public string DeviceCPUSpecifications => $"Ядер/Потоков {DeviceCPUInfo.NumberOfCores}/{DeviceCPUInfo.NumberOfLogicalProcessors}, " +
            $"Текущая/Базовая частота {DeviceCPUInfo.CurrentClockSpeed / 1000.0:F2}/{DeviceCPUInfo.MaxClockSpeed / 1000.0:F2} GHz";

        public DeviceGPUInfo DeviceGPUInfo { get; set; }
        public string DeviceGPUSpecifications => $"Объем {DeviceGPUInfo.VideoRAM / 1024.0 / 1024.0} MB";

        public DeviceRAMInfo DeviceRAMInfo { get; set; }
        public string DeviceRAMSpecifications => $"Тип {DeviceRAMInfo.Type}, Объем {DeviceRAMInfo.Size / 1024.0 / 1024.0 / 1024.0} " +
            $"({DeviceRAMInfo.AvailableSize / 1024.0 / 1024.0 / 1024.0:F2}) GB, Частота {DeviceRAMInfo.Speed} MHz";

        public DeviceDriveInfo DeviceDriveInfo { get; set; }
        public string DeviceDriveSpecifications => $"Объем/Доступно {DeviceDriveInfo.TotalSize / 1024.0 / 1024.0 / 1024.0:F2}/{DeviceDriveInfo.AvailableSize / 1024.0 / 1024.0 / 1024.0:F2} GB, Дисков {DeviceDriveInfo.DriversCount}";

        public DeviceViewModel(DeviceIdentityService identityService, DeviceInfoService infoService, DeviceIdentity? device = null)
        {
            _identityService = identityService;
            _infoService = infoService;

            CurrentDevice = device ?? _identityService.CurrentDevice;
            DeviceCPUInfo = _infoService.DeviceCPUInfo;
            DeviceGPUInfo = _infoService.DeviceGPUInfo;
            DeviceRAMInfo = _infoService.DeviceRAMInfo;
            DeviceDriveInfo = _infoService.DeviceDriveInfo;
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
            CurrentDevice = _identityService.CurrentDevice;
            DeviceCPUInfo = _infoService.DeviceCPUInfo;
            DeviceGPUInfo = _infoService.DeviceGPUInfo;
            DeviceRAMInfo = _infoService.DeviceRAMInfo;
            DeviceDriveInfo = _infoService.DeviceDriveInfo;
        }
    }
}

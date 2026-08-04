using LunktrionApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LunktrionApp.Services
{
    public class DeviceInfoService
    {
        private readonly HardwareService _hardwareService;

        public DeviceInfoService(HardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }

        public DeviceInfoService()
        {
            _hardwareService = null!;
        }

        public async Task<DeviceCPUInfo> GetDeviceCPUInfoAsync(bool refresh = false)
        {
            if (refresh)
                await _hardwareService.RefreshCPUList();

            var cpu = _hardwareService.Hardware.CpuList.FirstOrDefault();

            return cpu is null
                ? new DeviceCPUInfo()
                : new DeviceCPUInfo
                {
                    Name = cpu.Name,
                    NumberOfCores = cpu.NumberOfCores,
                    NumberOfLogicalProcessors = cpu.NumberOfLogicalProcessors,
                    CurrentClockSpeed = cpu.CurrentClockSpeed,
                    MaxClockSpeed = cpu.MaxClockSpeed
                };
        }

        public async Task<DeviceGPUInfo> GetDeviceGPUInfoAsync(bool refresh = false)
        {
            if (refresh)
                await _hardwareService.RefreshVideoControllerList();

            var gpu = _hardwareService.Hardware.VideoControllerList.FirstOrDefault();

            return gpu is null
                ? new DeviceGPUInfo()
                : new DeviceGPUInfo
                {
                    Name = gpu.Name,
                    VideoRAM = gpu.AdapterRAM
                };
        }

        public async Task<DeviceRAMInfo> GetDeviceRAMInfoAsync(bool refresh = false)
        {
            if (refresh)
            {
                await _hardwareService.RefreshMemoryStatus();
                await _hardwareService.RefreshMemoryList();
            }

            var ram = _hardwareService.Hardware.MemoryList.FirstOrDefault();

            return ram is null
                ? new DeviceRAMInfo()
                : new DeviceRAMInfo
                {
                    Size = _hardwareService.Hardware.MemoryList.Aggregate(0UL, (sum, next) => sum + next.Capacity),
                    AvailableSize = _hardwareService.Hardware.MemoryStatus.TotalPhysical,
                    Type = ram.MemoryType.ToString(),
                    Speed = ram.Speed
                };
        }

        public async Task<DeviceDriveInfo> GetDeviceDriveInfoAsync(bool refresh = false)
        {
            if (refresh)
                await _hardwareService.RefreshDriveList();

            var partitions = _hardwareService.Hardware.DriveList.SelectMany(d => d.PartitionList);

            return new DeviceDriveInfo
            {
                DriversCount = (uint)_hardwareService.Hardware.DriveList.Count,
                TotalSize = partitions.Aggregate(0UL, (sum, next) => sum + next.Size),
                AvailableSize = partitions.SelectMany(p => p.VolumeList).Aggregate(0UL, (sum, next) => sum + next.FreeSpace)
            };
        }
    }
}

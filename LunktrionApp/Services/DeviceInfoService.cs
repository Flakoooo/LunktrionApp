using Hardware.Info;
using LunktrionApp.Models;
using System.Linq;

namespace LunktrionApp.Services
{
    public class DeviceInfoService
    {
        private readonly HardwareInfo _hardware = new();

        public DeviceInfoService()
        {
            _hardware.RefreshCPUList();
            _hardware.RefreshVideoControllerList();
            _hardware.RefreshMemoryList();
            _hardware.RefreshMemoryStatus();
            _hardware.RefreshDriveList();
        }

        public DeviceCPUInfo DeviceCPUInfo
        {
            get
            {
                var cpu = _hardware.CpuList.FirstOrDefault();

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
        }

        public DeviceGPUInfo DeviceGPUInfo
        {
            get
            {
                var gpu = _hardware.VideoControllerList.FirstOrDefault();

                return gpu is null
                    ? new DeviceGPUInfo()
                    : new DeviceGPUInfo
                    {
                        Name = gpu.Name,
                        VideoRAM = gpu.AdapterRAM
                    };
            }
        }

        public DeviceRAMInfo DeviceRAMInfo
        {
            get
            {
                var ram = _hardware.MemoryList.FirstOrDefault();

                return ram is null
                    ? new DeviceRAMInfo()
                    : new DeviceRAMInfo
                    {
                        Size = _hardware.MemoryList.Aggregate(0UL, (sum, next) => sum + next.Capacity),
                        AvailableSize = _hardware.MemoryStatus.TotalPhysical,
                        Type = ram.MemoryType.ToString(),
                        Speed = ram.Speed
                    };
            }
        }

        public DeviceDriveInfo DeviceDriveInfo
        {
            get
            {
                var partitions = _hardware.DriveList.SelectMany(d => d.PartitionList);

                return new DeviceDriveInfo
                {
                    DriversCount = (uint)_hardware.DriveList.Count,
                    TotalSize = partitions.Aggregate(0UL, (sum, next) => sum + next.Size),
                    AvailableSize = partitions.SelectMany(p => p.VolumeList).Aggregate(0UL, (sum, next) => sum + next.FreeSpace)
                };
            }
        }
    }
}

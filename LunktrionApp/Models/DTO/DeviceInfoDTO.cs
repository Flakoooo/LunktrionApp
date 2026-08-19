using LunktrionApp.Models.Entities;

namespace LunktrionApp.Models.DTO
{
    public record class DeviceInfoDTO(
        DeviceCPUInfo CPUInfo, 
        DeviceGPUInfo GPUInfo, 
        DeviceRAMInfo RAMInfo, 
        DeviceDriveInfo DriveInfo
    );
}

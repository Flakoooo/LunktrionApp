namespace LunktrionApp.Models.Entities
{
    public record class DeviceGPUInfo(
        string Name = "ОШИБКА", 
        ulong VideoRAM = 0, 
        uint MaxRefreshRate = 0
    );
}

namespace LunktrionApp.Models
{
    public class DeviceGPUInfo
    {
        public string Name { get; init; } = "ОШИБКА";
        public ulong VideoRAM { get; init; } = 0;
        public uint MaxRefreshRate { get; init; } = 0;
    }
}

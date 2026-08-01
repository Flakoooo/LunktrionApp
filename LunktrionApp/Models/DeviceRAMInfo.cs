namespace LunktrionApp.Models
{
    public class DeviceRAMInfo
    {
        public ulong Size { get; init; } = 0;
        public ulong AvailableSize { get; init; } = 0;
        public string Type { get; init; } = "ОШИБКА";
        public uint Speed { get; init; } = 0;
    }
}

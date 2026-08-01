namespace LunktrionApp.Models
{
    public class DeviceDriveInfo
    {
        public uint DriversCount { get; init; } = 0;
        public ulong TotalSize { get; init; } = 0;
        public ulong AvailableSize { get; init; } = 0;
    }
}

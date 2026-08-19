namespace LunktrionApp.Models.Entities
{
    public record class DeviceDriveInfo(
        uint DriversCount, 
        ulong TotalSize, 
        ulong AvailableSize
    );
}

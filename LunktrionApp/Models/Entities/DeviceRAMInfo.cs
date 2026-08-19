namespace LunktrionApp.Models.Entities
{
    public record class DeviceRAMInfo(
        ulong Size = 0, 
        ulong AvailableSize = 0, 
        string Type = "ОШИБКА", 
        uint Speed = 0
    );
}

namespace LunktrionApp.Models
{
    public class DeviceCPUInfo
    {
        public string Name { get; init; } = "ОШИБКА";
        public uint NumberOfCores { get; init; } = 0;
        public uint NumberOfLogicalProcessors { get; init; } = 0;
        public uint CurrentClockSpeed { get; init; } = 0;
        public uint MaxClockSpeed { get; init; } = 0;
    }
}

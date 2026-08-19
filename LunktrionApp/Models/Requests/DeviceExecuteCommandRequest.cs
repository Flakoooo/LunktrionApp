namespace LunktrionApp.Models.Requests
{
    public record class DeviceExecuteCommandRequest(
        string TargetDeviceId,
        string RequestorDeviceId,
        string Command
    );
}

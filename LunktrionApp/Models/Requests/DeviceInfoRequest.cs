namespace LunktrionApp.Models.Requests
{
    public record class DeviceInfoRequest(
        string TargetDeviceId,
        string RequestorDeviceId
    );
}

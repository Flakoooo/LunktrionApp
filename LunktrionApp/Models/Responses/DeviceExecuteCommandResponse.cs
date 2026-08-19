namespace LunktrionApp.Models.Responses
{
    public record class DeviceExecuteCommandResponse(
        string Output,
        string RequestorDeviceId
    );
}

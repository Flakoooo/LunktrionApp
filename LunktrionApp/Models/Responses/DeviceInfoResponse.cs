using LunktrionApp.Models.DTO;

namespace LunktrionApp.Models.Responses
{
    public record class DeviceInfoResponse(
        DeviceInfoDTO Info,
        string RequestorDeviceId
    );
}

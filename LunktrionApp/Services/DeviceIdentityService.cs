using LunktrionApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LunktrionApp.Services
{
    public class DeviceIdentityService
    {
        private readonly HardwareService _hardwareService;

        public DeviceIdentityService(HardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }

        public DeviceIdentityService()
        {
            _hardwareService = null!;
        }

        public async Task<DeviceIdentity> GetCurrentDeviceAsync(bool refresh = false)
        {
            if (refresh)
            {
                await _hardwareService.RefreshComputerSystemList();
                await _hardwareService.RefreshOperatingSystem();
            }

            var computerSystem = _hardwareService.Hardware.ComputerSystemList.FirstOrDefault();

            return computerSystem is null
                ? new DeviceIdentity
                {
                    OSName = _hardwareService.Hardware.OperatingSystem.Name
                }
                : new DeviceIdentity
                {
                    Id = computerSystem.UUID,
                    DeviceName = computerSystem.Name,
                    OSName = _hardwareService.Hardware.OperatingSystem.Name,
                    Manufacturer = computerSystem.Vendor
                };
        }
    }
}

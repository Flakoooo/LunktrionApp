using Hardware.Info;
using LunktrionApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LunktrionApp.Services
{
    public class DeviceIdentityService
    {
        private readonly HardwareInfo _hardware = new();

        public DeviceIdentityService()
        {
            _hardware.RefreshComputerSystemList();
            _hardware.RefreshOperatingSystem();
        }

        public DeviceIdentity CurrentDevice
        {
            get
            {
                var computerSystem = _hardware.ComputerSystemList.FirstOrDefault();

                return computerSystem is null
                    ? new DeviceIdentity 
                    { 
                        OSName = _hardware.OperatingSystem.Name 
                    }
                    : new DeviceIdentity
                    {
                        Id = computerSystem.UUID,
                        DeviceName = computerSystem.Name,
                        OSName = _hardware.OperatingSystem.Name,
                        Manufacturer = computerSystem.Vendor
                    };
            }
        }
    }
}

using LunktrionApp.Hubs;
using LunktrionApp.Models.Requests;
using LunktrionApp.Models.Responses;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace LunktrionApp.Services
{
    public class CommandExecutorService : IDisposable
    {
        private readonly DeviceIdentityService _deviceIdentityService;
        private readonly MainHub _mainHub;

        public CommandExecutorService(
            DeviceIdentityService deviceIdentityService, 
            MainHub mainHub
        )
        {
            _deviceIdentityService = deviceIdentityService;
            _mainHub = mainHub;

            _mainHub.OnCommandReceived += ExecuteCommand;
        }

        public async Task<string> ExecuteWinCommandAsync(string command)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding(866)
                };

                using var process = new Process { StartInfo = startInfo };
                process.Start();

                var outputTask = process.StandardOutput.ReadToEndAsync();
                var errorTask = process.StandardError.ReadToEndAsync();

                await process.WaitForExitAsync();

                string output = await outputTask;
                string error = await errorTask;

                if (!string.IsNullOrEmpty(error))
                    return $"Ошибка: {error}";

                return output;
            }
            catch (Exception ex)
            {
                return $"Исключение при вызове: {ex.Message}";
            }
        }

        private async void ExecuteCommand(DeviceExecuteCommandRequest request)
        {
            var device = await _deviceIdentityService.GetCurrentDeviceAsync();

            if (device.DeviceId != request.TargetDeviceId) return;

            try
            {
                string result = await ExecuteWinCommandAsync(request.Command);

                await _mainHub.SendCommandResultAsync(
                    new DeviceExecuteCommandResponse(
                        result, 
                        request.RequestorDeviceId
                    )
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Критическая ошибка: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _mainHub.OnCommandReceived -= ExecuteCommand;
        }
    }
}

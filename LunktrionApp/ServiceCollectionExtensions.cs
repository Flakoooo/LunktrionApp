using LunktrionApp.Models.Interfaces;
using LunktrionApp.Services;
using LunktrionApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LunktrionApp
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            // Services
            collection.AddSingleton<NavigationService>();
            collection.AddSingleton<IAsyncInitializable>(
                sp => sp.GetRequiredService<NavigationService>());

            collection.AddSingleton<HardwareService>();
            collection.AddSingleton<DeviceIdentityService>();
            collection.AddSingleton<DeviceInfoService>();

            // ViewModels
            collection.AddSingleton<LoadingViewModel>();
            collection.AddSingleton<ActiveDevicesListViewModel>();
            collection.AddSingleton<NavigationPanelViewModel>();
            collection.AddTransient<MainViewModel>();
            collection.AddTransient<DeviceViewModel>();
            collection.AddTransient<DevicesListViewModel>();
        }

        public static async Task InitializeAsync(this IServiceProvider provider)
        {
            foreach (var service in provider.GetServices<IAsyncInitializable>())
            {
                await service.InitializeAsync();
            }
        }
    }
}

using LunktrionApp.Services;
using LunktrionApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace LunktrionApp
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            // Services
            collection.AddSingleton<NavigationService>();
            collection.AddSingleton<DeviceIdentityService>();
            collection.AddSingleton<DeviceInfoService>();

            // ViewModels
            collection.AddSingleton<ActiveDevicesListViewModel>();
            collection.AddSingleton<NavigationPanelViewModel>();
            collection.AddTransient<MainViewModel>();
            collection.AddTransient<DeviceViewModel>();
            collection.AddTransient<DevicesListViewModel>();
        }
    }
}

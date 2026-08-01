using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using LunktrionApp.Services;
using System;

namespace LunktrionApp.ViewModels;

public partial class MainViewModel : ViewModelBase, IDisposable
{
    private readonly NavigationService _navigationService;

    [ObservableProperty]
    public partial ViewModelBase? Navigation { get; set; }

    [ObservableProperty]
    public partial ViewModelBase? ActiveDevicesList { get; set; }

    [ObservableProperty]
    public partial ViewModelBase? CurrentPage { get; set; }

    public MainViewModel(
        NavigationPanelViewModel navigationPanelViewModel,
        ActiveDevicesListViewModel activeDevicesListViewModel,
        NavigationService navigationService
    )
    {
        Navigation = navigationPanelViewModel;
        ActiveDevicesList = activeDevicesListViewModel;
        _navigationService = navigationService;

        _navigationService.CurrentPageChanged += ChangeCurrentPage;

        _navigationService.Navigate<DeviceViewModel>();
    }

    public MainViewModel()
    {
        if (!Design.IsDesignMode)
        {
            throw new InvalidOperationException(
                "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
            );
        }

        _navigationService = new NavigationService();
        Navigation = new NavigationPanelViewModel();
        ActiveDevicesList = new ActiveDevicesListViewModel();
        CurrentPage = new DeviceViewModel();
    }

    private void ChangeCurrentPage()
    {
        CurrentPage = _navigationService.CurrentPage;
    }


    public void Dispose()
    {
        _navigationService.CurrentPageChanged -= ChangeCurrentPage;
    }
}

using Avalonia.Controls;
using LunktrionApp.Services;
using System;

namespace LunktrionApp.ViewModels
{
    public class NavigationPanelViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;

        public NavigationPanelViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public NavigationPanelViewModel()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
                );
            }

            _navigationService = new NavigationService();
        }
    }
}

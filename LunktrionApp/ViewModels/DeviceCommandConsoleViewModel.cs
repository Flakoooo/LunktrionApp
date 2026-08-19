using Avalonia.Controls;
using LunktrionApp.Hubs;
using LunktrionApp.Models.Entities;
using System;
using System.Collections.ObjectModel;

namespace LunktrionApp.ViewModels
{
    public class DeviceCommandConsoleViewModel : ViewModelBase
    {
        private readonly MainHub _mainHub;

        private readonly ObservableCollection<ConsoleLogItem> _logs = [];

        public DeviceCommandConsoleViewModel(MainHub mainHub)
        {
            _mainHub = mainHub;
        }

        public DeviceCommandConsoleViewModel()
        {
            if (!Design.IsDesignMode)
            {
                throw new InvalidOperationException(
                    "Этот конструктор предназначен только для дизайнера Avalonia и не должен вызываться в рантайме"
                );
            }

            _mainHub = null!;
        }
    }
}

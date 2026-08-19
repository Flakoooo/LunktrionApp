using LunktrionApp.Models.Enums;
using System;

namespace LunktrionApp.Models.Entities
{
    public class ConsoleLogItem
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;
        public ConsoleMessageType Type { get; set; }
    }
}

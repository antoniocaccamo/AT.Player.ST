using Stylet;
using System;

namespace AT.Player.Pages.Monitors
{
    public abstract class AbstractMonitorViewModel : Screen
    {
        protected readonly string _channel;

        public AbstractMonitorViewModel(string channel)
        {
            _channel = channel;
        }

        public Uri Source { get; set; }
    }
}
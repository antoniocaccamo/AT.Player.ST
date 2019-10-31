using Stylet;
using System;
using System.Windows.Threading;

namespace AT.Player.Pages.Monitors
{
    public abstract class AbstractMonitorViewModel : Screen
    {
        #region Private Fields

        private string _channel;
        protected readonly IEventAggregator _events;

        protected DispatcherTimer _timer = new DispatcherTimer();

        protected AbstractMonitorViewModel(IEventAggregator events)
        {
            _events = events;
        }

        #endregion Private Fields

        #region Public Properties

        public string Channel { get => _channel; set => _channel = value; }

        public MonitorViewModel MonitorViewModel { get; set; }

        //public IEventAggregator EventAggregator => _events;

        public virtual Uri Source { get; set; }

        #endregion Public Properties
    }
}
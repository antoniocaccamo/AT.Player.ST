using Stylet;
using System;
using System.Windows.Threading;

namespace AT.Player.Pages.Monitors
{
    public abstract class AbstractMonitorViewModel : Screen
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private string _channel;
        protected Uri _source;
        protected readonly IEventAggregator _events;

        protected readonly DispatcherTimer _timer;

        protected AbstractMonitorViewModel(IEventAggregator events)
        {
            _events = events;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
        }

        #endregion Private Fields

        #region Public Properties

        public string Channel { get => _channel; set => _channel = value; }

        public MonitorViewModel MonitorViewModel { get; set; }

        //public IEventAggregator EventAggregator => _events;

        public virtual Uri Source
        {
            get => _source;
            set => _source = value;
        }

        public virtual void Play()
        {
            //_timer.Stop();
            //_timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Start();
            _logger.Warn("{0} :  _timer.IsEnabled : {1} ", _channel, _timer.IsEnabled);
        }
    }

    #endregion Public Properties
}
namespace AT.Player.Pages.Settings
{
    using Stylet;

    public class MonitorSettingViewModel : Screen
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly string _channel;
        private readonly IEventAggregator _events;
        private readonly Configuration.Monitor _monitor;

        //private readonly IMonitorViewModelFactory _monitorViewModelFactory;
        private readonly IWindowManager _windowManager;

        private Monitors.MonitorViewModel _monitorVM;

        #endregion Private Fields

        #region Public Constructors

        public MonitorSettingViewModel(IWindowManager windowManager, IEventAggregator events, string channel, Configuration.Monitor monitor)
        {
            this._windowManager = windowManager;
            this._events = events;
            this._channel = channel;
            this._monitor = monitor;

            DisplayName = "Monitor Manager";
            _logger.Warn("###### " + DisplayName);
        }

        #endregion Public Constructors

        #region Public Properties

        public Configuration.Monitor Monitor => _monitor;

        public double Height
        {
            get => _monitor.Size.Height;
            set => _monitor.Size.Height = value;
        }

        public double Left
        {
            get => _monitor.Location.Left;
            set => _monitor.Location.Left = value;
        }

        public double Top
        {
            get => _monitor.Location.Top;
            set => _monitor.Location.Top = value;
        }

        public double Width
        {
            get => _monitor.Size.Width;
            set
            {
                _logger.Warn("width changed {0}", value);
                _monitor.Size.Width = value;
            }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnClose()
        {
            _logger.Info("_monitorVM.RequestClose() ...");
            _monitorVM.RequestClose();
            base.OnClose();
        }

        protected override void OnInitialActivate()
        {
            _monitorVM = // _monitorViewModelFactory.CreateMonitorViewModel(this._channel);
                new Monitors.MonitorViewModel(_events, _channel, _monitor);
            this._windowManager.ShowWindow(_monitorVM);
        }

        #endregion Protected Methods
    }
}
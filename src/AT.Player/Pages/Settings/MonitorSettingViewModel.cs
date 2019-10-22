namespace AT.Player.Pages.Settings
{
    using Stylet;

    public class MonitorSettingViewModel : Screen
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly string _channel;
        private readonly Configuration.Monitor _monitor;
        private readonly IMonitorViewModelFactory _monitorViewModelFactory;
        private readonly IWindowManager _windowManager;

        private Monitors.MonitorViewModel _monitorVM;

        #endregion Private Fields

        #region Public Constructors

        public MonitorSettingViewModel(IWindowManager windowManager, IMonitorViewModelFactory monitorViewModelFactory, string channel, Configuration.Monitor monitor)
        {
            this._windowManager = windowManager;
            //this._monitorViewModelFactory = monitorViewModelFactory;
            this._channel = channel;
            this._monitor = monitor;

            DisplayName = "Monitor Manager";
            _logger.Warn("###### " + DisplayName);
        }

        #endregion Public Constructors

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
                new Monitors.MonitorViewModel();
            this._windowManager.ShowWindow(_monitorVM);
        }

        #endregion Protected Methods
    }
}
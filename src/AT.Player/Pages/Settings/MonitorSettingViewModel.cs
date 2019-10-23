namespace AT.Player.Pages.Settings
{
    using Stylet;

    public class MonitorSettingViewModel : Screen
    {
        #region Private Fields

        private static readonly BindableCollection<LabelledValue<Configuration.Activation.ActivationEnum>> _activationEnum =
            new BindableCollection<LabelledValue<Configuration.Activation.ActivationEnum>>()
            {
                LabelledValue.Create("All Day", Configuration.Activation.ActivationEnum.ALLDAY),
                LabelledValue.Create("Timed", Configuration.Activation.ActivationEnum.TIMED)
            };

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>> _whenNotActiveEnum =
            new BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>>()
            {
                LabelledValue.Create("Black", Configuration.Activation.WhenNotActiveEnum.BLACK),
                LabelledValue.Create("Image", Configuration.Activation.WhenNotActiveEnum.IMAGE),
                LabelledValue.Create("Watch", Configuration.Activation.WhenNotActiveEnum.WATCH)
            };

        private readonly string _channel;
        private readonly IEventAggregator _events;
        private readonly Configuration.Monitor _monitor;

        //private readonly IMonitorViewModelFactory _monitorViewModelFactory;
        private readonly IWindowManager _windowManager;

        private Monitors.MonitorViewModel _monitorVM;

        private LabelledValue<Configuration.Activation.ActivationEnum> _selectedActivationEnum;
        private LabelledValue<Configuration.Activation.WhenNotActiveEnum> _selectedWhenNotActiveEnum;

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

            _selectedActivationEnum = new LabelledValue<Configuration.Activation.ActivationEnum>() { Value = _monitor.Activation.Type };
            _selectedWhenNotActiveEnum = new LabelledValue<Configuration.Activation.WhenNotActiveEnum>() { Value = _monitor.Activation.WhenNotActive };
        }

        #endregion Public Constructors

        #region Public Properties

        public BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>> WhenNotActiveEnumValues => _whenNotActiveEnum;
        public BindableCollection<LabelledValue<Configuration.Activation.ActivationEnum>> ActivationEnumValues => _activationEnum;

        //public double Height
        //{
        //    get => _monitor.Size.Height;
        //    set => _monitor.Size.Height = value;
        //}

        //public double Left
        //{
        //    get => _monitor.Location.Left;
        //    set => _monitor.Location.Left = value;
        //}

        public Configuration.Monitor Monitor => _monitor;

        public LabelledValue<Configuration.Activation.ActivationEnum> SelectedActivationEnum
        {
            get => _selectedActivationEnum;
            set { _selectedActivationEnum = value; _monitor.Activation.Type = value.Value; }
        }

        public LabelledValue<Configuration.Activation.WhenNotActiveEnum> SelectedWhenNotActiveEnum
        {
            get => _selectedWhenNotActiveEnum;
            set { _selectedWhenNotActiveEnum = value; _monitor.Activation.WhenNotActive = value.Value; }
        }

        //public double Top
        //{
        //    get => _monitor.Location.Top;
        //    set => _monitor.Location.Top = value;
        //}

        //public double Width
        //{
        //    get => _monitor.Size.Width;
        //    set
        //    {
        //        _logger.Warn("width changed {0}", value);
        //        _monitor.Size.Width = value;
        //    }
        //}

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
namespace AT.Player.Pages.Settings
{
    using AT.Player.Callbacks;
    using AT.Player.Events;
    using AT.Player.Helpers;
    using AT.Player.Model;

    //using AT.Player.Service;
    using Stylet;
    using System;
    using System.Threading;
    using System.Windows;

    public class MonitorSettingViewModel : Screen, IHandle<Events.MonitorShowMediaErrorEvent>
    {
        #region Private Fields

        private static readonly BindableCollection<LabelledValue<Configuration.Activation.ActivationEnum>> _activationEnum =
            new BindableCollection<LabelledValue<Configuration.Activation.ActivationEnum>>()
            {
                LabelledValue.Create(Configuration.Activation.ActivationEnum.ALLDAY.ToString(), Configuration.Activation.ActivationEnum.ALLDAY),
                LabelledValue.Create(Configuration.Activation.ActivationEnum.TIMED.ToString(), Configuration.Activation.ActivationEnum.TIMED)
            };

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>> _whenNotActiveEnum =
            new BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>>()
            {
                LabelledValue.Create(Configuration.Activation.WhenNotActiveEnum.BLACK.ToString(), Configuration.Activation.WhenNotActiveEnum.BLACK),
                LabelledValue.Create(Configuration.Activation.WhenNotActiveEnum.IMAGE.ToString(), Configuration.Activation.WhenNotActiveEnum.IMAGE),
                LabelledValue.Create(Configuration.Activation.WhenNotActiveEnum.WATCH.ToString(), Configuration.Activation.WhenNotActiveEnum.WATCH)
            };

        private readonly string _channel;

        private readonly IEventAggregator _events;

        private readonly Configuration.Monitor _monitor;

        private readonly PalimpsestLooper _palimpsestLooper;

        //private readonly IMonitorViewModelFactory _monitorViewModelFactory;
        private readonly IWindowManager _windowManager;

        private DateTime _currentMediaPausedDateTime;

        private DateTime _currentMediaStartDateTime;

        private MonitorStatusEnum _monitorStatus;

        private Monitors.MonitorViewModel _monitorVM;

        private LabelledValue<Configuration.Activation.ActivationEnum> _selectedActivationEnum;

        private LabelledValue<Configuration.Activation.WhenNotActiveEnum> _selectedWhenNotActiveEnum;

        //private PalimpsestService _palimpsestService = new PalimpsestService();
        private Timer _timer;

        #endregion Private Fields

        #region Public Constructors

        public MonitorSettingViewModel(IWindowManager windowManager, IEventAggregator events
            //, string channel, Configuration.Monitor monitor
            )
        {
            this._windowManager = windowManager;
            this._events = events;
            //this._channel = channel;
            //this._monitor = monitor;

            DisplayName = "Monitor Manager";
            _logger.Warn("###### " + DisplayName);

            _selectedActivationEnum = LabelledValue.Create(_monitor?.Activation.Type.ToString(), _monitor.Activation.Type);
            _selectedWhenNotActiveEnum = LabelledValue.Create(_monitor?.Activation.WhenNotActive.ToString(), _monitor.Activation.WhenNotActive);

            _monitorStatus = MonitorStatusEnum.NOT_ACTIVE;
            _palimpsestLooper = new PalimpsestLooper();
        }

        #endregion Public Constructors

        #region Public Enums

        public enum MonitorStatusEnum
        {
            NOT_ACTIVE,
            STOPPED,
            PLAYING,
            PAUSED
        };

        #endregion Public Enums

        #region Public Properties

        public BindableCollection<LabelledValue<Configuration.Activation.ActivationEnum>> ActivationEnumValues => _activationEnum;

        public Media CurrentMedia { get; private set; }

        public Configuration.Monitor Monitor => _monitor;

        public MonitorStatusEnum MonitorStatus { get => _monitorStatus; private set => _monitorStatus = value; }

        Palimpsest Palimpsest => _palimpsestLooper.Palimpsest;

        //public double Left
        //{
        //    get => _monitor.Location.Left;
        //    set => _monitor.Location.Left = value;
        //}
        public LabelledValue<Configuration.Activation.ActivationEnum> SelectedActivationEnum
        {
            get => _selectedActivationEnum;
            set { _selectedActivationEnum = value; _monitor.Activation.Type = value.Value; }
        }

        //public double Height
        //{
        //    get => _monitor.Size.Height;
        //    set => _monitor.Size.Height = value;
        //}
        public LabelledValue<Configuration.Activation.WhenNotActiveEnum> SelectedWhenNotActiveEnum
        {
            get => _selectedWhenNotActiveEnum;
            set { _selectedWhenNotActiveEnum = value; _monitor.Activation.WhenNotActive = value.Value; }
        }

        public BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>> WhenNotActiveEnumValues => _whenNotActiveEnum;

        #endregion Public Properties

        #region Public Methods

        public bool CanDoPlay()
        {
            return MonitorStatusEnum.NOT_ACTIVE.Equals(MonitorStatus) || MonitorStatusEnum.STOPPED.Equals(MonitorStatus);
        }

        public void DoPlay()
        {
            Next();
            MonitorStatus = MonitorStatusEnum.PLAYING;
        }

        public bool CanDoPause()
        {
            return MonitorStatusEnum.PLAYING.Equals(MonitorStatus);
        }

        public void DoPause()
        {
            //Next();
            MonitorStatus = MonitorStatusEnum.PAUSED;
        }

        public bool CanDoStop()
        {
            return MonitorStatusEnum.PLAYING.Equals(MonitorStatus);
        }

        public void DoStop()
        {
            //Next();
            MonitorStatus = MonitorStatusEnum.STOPPED;
        }

        public void Next()
        {
            //_timer?.Dispose();
            //ProgressChanged(this, new ProgressChangedEventArgs(0, null));
            CurrentMedia = _palimpsestLooper.Next;
            _currentMediaStartDateTime = DateTime.Now;
            _currentMediaPausedDateTime = DateTime.MinValue;
            //_worker.RunWorkerAsync();
            ShowMedia();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnClose()
        {
            _logger.Info("_monitorVM.RequestClose() ...");
            _monitorVM?.RequestClose();
            base.OnClose();
        }

        //public double Width
        //{
        //    get => _monitor.Size.Width;
        //    set
        //    {
        //        _logger.Warn("width changed {0}", value);
        //        _monitor.Size.Width = value;
        //    }
        //}
        protected override void OnInitialActivate()
        {
            _monitorVM = // _monitorViewModelFactory.CreateMonitorViewModel(this._channel);
                new Monitors.MonitorViewModel(_events, _channel, _monitor);
            Execute.OnUIThreadAsync( 
                new Action( ()=> this._windowManager.ShowWindow(_monitorVM))
                );

            if (!string.IsNullOrEmpty(_monitor.Sequence))
            {
                var palimpsest = //_palimpsestService.GetAsync(_monitor.Sequence).Result
                    PalimpsestHelper.Get(_monitor.Sequence);

                _logger.Info("palimpsest : {0}", palimpsest);
                _palimpsestLooper.Palimpsest = palimpsest;

                MonitorSettingTimingCallback stc = new MonitorSettingTimingCallback(this);
                TimerCallback tc = new TimerCallback(stc.timingCallBack);
                _timer = new Timer(tc, null, 100, 500);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void ShowMedia()
        {
            Execute.Dispatcher.Post (new Action(
                () => _events.Publish(new Events.MonitorShowMediaEvent(CurrentMedia), _channel)));
            //this._events.PublishOnUIThread(new Events.MonitorShowMediaEvent(CurrentMedia), _channel);
        }

        void IHandle<MonitorShowMediaErrorEvent>.Handle(MonitorShowMediaErrorEvent evt)
        {
            _logger.Error($"{_channel} : can't play media : {evt.Media}");
            Next();
        }

        #endregion Private Methods

        //public double Top
        //{
        //    get => _monitor.Location.Top;
        //    set => _monitor.Location.Top = value;
        //}
    }
}
namespace AT.Player.Pages.Settings
{
    using AT.Player.Callbacks;
    using AT.Player.Events;
    using AT.Player.Helpers;
    using AT.Player.Model;
    using AT.Player.Pages.Monitors;

    //using AT.Player.Service;
    using Stylet;
    using System;
    using System.Threading;
    using System.Windows;

    public class MonitorSettingViewModel : Screen, IHandle<Events.MonitorShowMediaErrorEvent>, IHandle<MediaProgressChangeEvent>
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

        private string _channel;

        private readonly IEventAggregator _events;

        private Configuration.Monitor _monitor;

        private readonly PalimpsestLooper _palimpsestLooper;

        //private readonly IMonitorViewModelFactory _monitorViewModelFactory;
        private readonly IWindowManager _windowManager;

        private DateTime _currentMediaPausedDateTime;

        private DateTime _currentMediaStartDateTime;

        private MonitorStatusEnum _monitorStatus;

        private Monitors.MonitorViewModel _monitorViewModel;

        private LabelledValue<Configuration.Activation.ActivationEnum> _selectedActivationEnum;

        private LabelledValue<Configuration.Activation.WhenNotActiveEnum> _selectedWhenNotActiveEnum;

        private Timer _timer;

        #endregion Private Fields

        #region Public Constructors

        public MonitorSettingViewModel(IWindowManager windowManager, IEventAggregator events, MonitorViewModel monitorViewModel)
        {
            this._windowManager = windowManager;
            this._events = events;
            this._monitorViewModel = monitorViewModel;
            //this._channel = channel;
            //this._monitor = monitor;

            DisplayName = "Monitor Manager";
            _logger.Warn("###### " + DisplayName);

            _monitorStatus = MonitorStatusEnum.NOT_ACTIVE;
            _palimpsestLooper = new PalimpsestLooper();
        }

        #endregion Public Constructors

        public event MonitorShowMediaEvent CurrentMediaChanged;

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

        public BindableCollection<LabelledValue<Configuration.Activation.WhenNotActiveEnum>> WhenNotActiveEnumValues => _whenNotActiveEnum;

        public int CurrentProgress { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void ChannelAndMonitor(string channel, Configuration.Monitor monitor)
        {
            this._channel = channel;
            this._monitor = monitor;
            this._monitorViewModel.ChannelAndMonitor(_channel, _monitor);
            _selectedActivationEnum = LabelledValue.Create(_monitor.Activation.Type.ToString(), _monitor.Activation.Type);
            _selectedWhenNotActiveEnum = LabelledValue.Create(_monitor.Activation.WhenNotActive.ToString(), _monitor.Activation.WhenNotActive);

            var palimpsest = PalimpsestHelper.Get(_monitor?.Sequence);
            if (palimpsest != null)
            {
                _palimpsestLooper.Palimpsest = palimpsest;
            }

            this._events.Subscribe(this, _channel);
        }

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
            if (_palimpsestLooper.Palimpsest != null)
            {
                CurrentMedia = _palimpsestLooper.Next;
                _currentMediaStartDateTime = DateTime.Now;
                _currentMediaPausedDateTime = DateTime.MinValue;
                //_worker.RunWorkerAsync();
                ShowMedia();
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnClose()
        {
            _logger.Info("_monitorVM.RequestClose() ...");
            _monitorViewModel?.RequestClose();
            base.OnClose();
        }

        public void ShowMonitor()
        {
            Execute.PostToUIThread(() => _windowManager.ShowWindow(this._monitorViewModel));
        }

        #endregion Protected Methods

        #region Private Methods

        private void ShowMedia()
        {
            Execute.PostToUIThread(() => CurrentProgress = 0);
            Execute.PostToUIThread(() => _events.Publish(new Events.MonitorShowMediaEvent(CurrentMedia), _channel));
            //this._events.PublishOnUIThread(new Events.MonitorShowMediaEvent(CurrentMedia), _channel);
        }

        void IHandle<MonitorShowMediaErrorEvent>.Handle(MonitorShowMediaErrorEvent evt)
        {
            _logger.Error($"{_channel} : can't play media : {evt.Media}");
            Next();
        }

        void IHandle<MediaProgressChangeEvent>.Handle(MediaProgressChangeEvent evt)
        {
            _logger.Error($"{_channel} : progress changed : {evt.ProgressPercentage}");
            Execute.PostToUIThread(() => CurrentProgress = evt.ProgressPercentage);
        }

        #endregion Private Methods

        //public double Top
        //{
        //    get => _monitor.Location.Top;
        //    set => _monitor.Location.Top = value;
        //}

        protected override void OnInitialActivate()
        {
            MonitorSettingTimingCallback stc = new MonitorSettingTimingCallback(this);
            TimerCallback tc = new TimerCallback(stc.timingCallBack);
            _timer = new Timer(tc, null, 100, 500);

            _monitorViewModel.ProgressChanged += (snd, evt) =>
            {
                _logger.Error($"{_channel} : progress changed : {evt.ProgressPercentage}");
                Execute.PostToUIThread(() => CurrentProgress = evt.ProgressPercentage);
            };
        }
    }
}
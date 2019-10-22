﻿namespace AT.Player.Pages
{
    using AT.Player.Configuration;
    using AT.Player.Events;
    using AT.Player.Pages.Settings;
    using AT.Player.Service;
    using Stylet;

    public class ShellViewModel : Conductor<IScreen>.Collection.AllActive, IHandle<IShowEvent>
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IPreferenceService _configurationService;

        private readonly MonitorGroupSettingViewModel _monitorGroup;
        private readonly SequenceGroupSettingViewModel _sequenceGroup;
        private readonly TaskbarViewModel _taskbar;
        private readonly WeatherSettingViewModel _weather;
        private IContext _context;

        #endregion Private Fields

        #region Public Constructors

        public ShellViewModel(IEventAggregator events, IPreferenceService configurationService, IContext context,
            TaskbarViewModel taskbarViewModel,
            MonitorGroupSettingViewModel monitorGroupViewModel, SequenceGroupSettingViewModel sequenceGroupViewModel, WeatherSettingViewModel weatherViewModel)
        {
            this.DisplayName = "AT Player";
            this._taskbar = taskbarViewModel;
            this._monitorGroup = monitorGroupViewModel;
            this._sequenceGroup = sequenceGroupViewModel;
            this._weather = weatherViewModel;

            this._configurationService = configurationService;
            this._context = context;

            events.Subscribe(this);

            Items.Add(_monitorGroup);
            Items.Add(_sequenceGroup);
            Items.Add(_weather);

            this.ActivateItem(_weather);
        }

        #endregion Public Constructors

        #region Public Properties

        public double Height
        {
            get => _context.Configuration.Size.Height;
            set => _context.Configuration.Size.Height = value;
        }

        public double Left
        {
            get => _context.Configuration.Location.Left;
            set => _context.Configuration.Location.Left = value;
        }

        public TaskbarViewModel Taskbar => _taskbar;

        public double Top
        {
            get => _context.Configuration.Location.Top;
            set => _context.Configuration.Location.Top = value;
        }

        public double Width
        {
            get => _context.Configuration.Size.Width;
            set => _context.Configuration.Size.Width = value;
        }

        #endregion Public Properties

        #region Public Methods

        public void Handle(IShowEvent message)
        {
            if (message.GetType() == typeof(ShowWeatherSettingEvent))
                this.ActivateItem(_weather);

            this.ActivateItem(_monitorGroup);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnClose()
        {
            _logger.Info("_context.Configuration : {}", _context.Configuration);
            base.OnClose();
        }

        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();
            _logger.Info("reading configuration...");
            _context.Configuration =
                _configurationService.Get();

            _logger.Info("Top {}", Top);
            _logger.Info("Left {}", Left);
        }

        #endregion Protected Methods
    }
}
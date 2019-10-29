using AT.Player.Configuration;
using AT.Player.Pages.Monitors;
using Stylet;

namespace AT.Player.Pages.Settings
{
    public class MonitorGroupSettingViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IContext _context;
        private readonly IEventAggregator _events;
        private readonly IWindowManager _windowManager;

        #endregion Private Fields

        private readonly IMonitorViewModelFactory _factory;

        #region Public Constructors

        public MonitorGroupSettingViewModel(IWindowManager windowManager, IEventAggregator events, IContext context
            , IMonitorViewModelFactory factory
            )
        {
            DisplayName = "Monitors Manager";
            _logger.Warn("###### " + DisplayName);
            this._windowManager = windowManager;
            this._events = events;
            this._context = context;
            this._factory = factory;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void OnInitialActivate()
        {
            //MonitorSettingViewModel first = null;
            using (_context)
            {
                uint mnt = 1;
                foreach (var _monitor in _context.Configuration.Monitors)
                {
                    var _channel = $"monitor #{mnt}";
                    MonitorSettingViewModel _msvm = _factory.CreateMonitorSettingViewModel();
                    //MonitorViewModel mvm = new MonitorViewModel(_events);
                    _msvm.DisplayName += $" | {_channel}";
                    _msvm.ChannelAndMonitor(_channel, _monitor);

                    Items.Add(_msvm);
                    mnt++;
                    this.ActivateItem(_msvm);

                    _msvm.ShowMonitorAsync();

                    //_logger.Warn("###### send evts");

                    //mnt = 1;
                    //channel = $"monitor #{mnt}";
                    //var evti = new Events.MonitorShowImageEvent() { Source = new System.Uri(_context.Configuration.Dummy.Image) };
                    //_events.Publish(evti, channel);

                    //mnt++;
                    //channel = $"monitor #{mnt}";
                    //var evtv = new Events.MonitorShowVideoEvent() { Source = new System.Uri(_context.Configuration.Dummy.Video) };
                    //_events.Publish(evtv, channel);
                }
            }
        }

        #endregion Protected Methods
    }
}
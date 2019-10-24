using AT.Player.Configuration;
using Stylet;

namespace AT.Player.Pages.Settings
{
    public class MonitorGroupSettingViewModel : Conductor<IScreen>.Collection.AllActive
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IContext _context;
        private readonly IEventAggregator _events;
        private readonly IWindowManager _windowManager;

        #endregion Private Fields

        //private readonly IMonitorSettingViewModelFactory monitorSettingViewModelFactory;

        #region Public Constructors

        public MonitorGroupSettingViewModel(IWindowManager windowManager, IEventAggregator events, IContext context
            //7IMonitorSettingViewModelFactory monitorSettingViewModelFactory
            )
        {
            DisplayName = "Monitors Manager";
            _logger.Warn("###### " + DisplayName);
            this._windowManager = windowManager;
            this._events = events;
            this._context = context;
            //this.monitorSettingViewModelFactory = monitorSettingViewModelFactory;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void OnInitialActivate()
        {
            //MonitorSettingViewModel first = null;
            using (_context)
            {
                uint mnt = 1;
                foreach (Configuration.Monitor _monitor in _context.Configuration.Monitors)
                {
                    var channel = $"monitor #{mnt}";
                    MonitorSettingViewModel m =
                        // monitorSettingViewModelFactory.CreateMonitorSettingViewModel(channel, _monitor)
                        new MonitorSettingViewModel(_windowManager, _events, channel, _monitor)
                    ;
                    m.DisplayName += $" | {channel}";

                    Items.Add(m);
                    mnt++;
                    this.ActivateItem(Items[0]);

                    _logger.Warn("###### send evts");

                    mnt = 1;
                    channel = $"monitor #{mnt}";
                    var evti = new Events.MonitorShowImageEvent() { Source = new System.Uri(_context.Configuration.Dummy.Image) };
                    _events.Publish(evti, channel);

                    mnt++;
                    channel = $"monitor #{mnt}";
                    var evtv = new Events.MonitorShowVideoEvent() { Source = new System.Uri(_context.Configuration.Dummy.Video) };
                    _events.Publish(evtv, channel);
                }
            }
        }

        #endregion Protected Methods
    }
}
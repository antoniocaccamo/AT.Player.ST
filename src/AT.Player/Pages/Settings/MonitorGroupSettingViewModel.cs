using AT.Player.Configuration;
using Stylet;

namespace AT.Player.Pages.Settings
{
    public class MonitorGroupSettingViewModel : Conductor<IScreen>.Collection.AllActive
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IWindowManager _windowManager;
        private readonly IMonitorSettingViewModelFactory _monitorViewModelFactory;
        private readonly IContext _context;

        public MonitorGroupSettingViewModel(IWindowManager windowManager, IContext context, IMonitorSettingViewModelFactory monitorViewModelFactory)
        {
            DisplayName = "Monitors Manager";
            Logger.Warn("###### " + DisplayName);
            this._windowManager = windowManager;
            this._monitorViewModelFactory = monitorViewModelFactory;
            this._context = context;
        }

        protected override void OnInitialActivate()
        {
            using (_context)
            {
                uint mnt = 1;
                foreach (Configuration.Monitor _monitor in _context.Configuration.Monitors)
                {
                    var channel = $"monitor #{mnt}";
                    MonitorSettingViewModel m = // _monitorViewModelFactory.CreateMonitorSettingViewModel(channel);
                        new MonitorSettingViewModel(_windowManager, null, channel, _monitor);
                    m.DisplayName += $" | {channel}";
                    Items.Add(m);
                    mnt++;
                }
            }
        }
    }
}
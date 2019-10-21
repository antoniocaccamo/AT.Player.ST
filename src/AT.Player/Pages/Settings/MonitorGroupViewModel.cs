using Stylet;

namespace AT.Player.Pages.Settings
{
    public class MonitorGroupViewModel : Conductor<IScreen>.Collection.AllActive
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMonitorSettingViewModelFactory _monitorViewModelFactory;

        public MonitorGroupViewModel(IWindowManager windowManager, IMonitorSettingViewModelFactory monitorViewModelFactory)
        {
            DisplayName = "Monitors Manager";
            Logger.Warn("###### " + DisplayName);
            this._monitorViewModelFactory = monitorViewModelFactory;

            MonitorSettingViewModel m = _monitorViewModelFactory.CreateMonitorSettingViewModel();
            m.DisplayName += "| Monitor #1";
            Items.Add(m);

            m = _monitorViewModelFactory.CreateMonitorSettingViewModel();
            m.DisplayName += "| Monitor #2";
            Items.Add(m);
        }
    }
}
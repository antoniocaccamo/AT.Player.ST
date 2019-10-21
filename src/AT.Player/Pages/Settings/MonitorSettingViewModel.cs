using AT.Player.Pages.Monitors;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class MonitorSettingViewModel : Screen
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly MonitorViewModel _monitor;

        public MonitorSettingViewModel(IWindowManager windowManager, IMonitorViewModelFactory monitorViewModelFactory)
        {
            DisplayName = "Monitor Manager";
            Logger.Warn("###### " + DisplayName);

            this._monitor = monitorViewModelFactory.CreateMonitorViewModel();

            windowManager.ShowWindow(_monitor);
        }

        protected override void OnClose()
        {
            _monitor.CloseWith(this);
            base.OnClose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class MonitorGroupViewModel : Stylet.Screen
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public MonitorGroupViewModel() : base()
        {
            DisplayName = "Monitors Manager";
            Logger.Warn("###### " + DisplayName);
        }
    }
}
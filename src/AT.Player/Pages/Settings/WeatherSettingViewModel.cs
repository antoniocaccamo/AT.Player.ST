using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class WeatherSettingViewModel : Screen
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public WeatherSettingViewModel() : base()

        {
            DisplayName = "Weather Manager";
            Logger.Warn("###### " + DisplayName);
        }
    }
}
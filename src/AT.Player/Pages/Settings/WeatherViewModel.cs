using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Settings
{
    public class WeatherViewModel : Screen
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public WeatherViewModel() : base()

        {
            DisplayName = "Weather Manager";
            Logger.Warn("###### " + DisplayName);
        }
    }
}
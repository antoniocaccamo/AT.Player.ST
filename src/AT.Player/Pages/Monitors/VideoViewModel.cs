using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Monitors
{
    public class VideoViewModel : AbstractMonitorViewModel
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void MediaElement_MediaFailed(object sender, Unosquare.FFME.Common.MediaFailedEventArgs e)
        {
            _logger.Error(e.ErrorException, $"error playing video");
        }
    }
}
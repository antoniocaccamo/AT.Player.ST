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

        public VideoViewModel(string channel) : base(channel)
        {
        }

        public void MediaElement_MediaFailed(object sender, Unosquare.FFME.Common.MediaFailedEventArgs e)
        {
            _logger.Error($"{_channel} : error playing video : {e.ErrorException}");
        }

        public void MediaElement_MediaOpened(object sender, Unosquare.FFME.Common.MediaOpenedEventArgs e)
        {
            _logger.Info($" {_channel} : media opened : {e.Info.MediaSource} Duration : {e.Info.Duration}");
        }

        public void MediaElement_MediaEnded(object sender, EventArgs e)
        {
            _logger.Info($" {_channel} : media endend : sender  {sender}");
        }

        public void MediaElement_PositionChanged(object sender, Unosquare.FFME.Common.PositionChangedEventArgs e)
        {
            _logger.Info($"{_channel} :  PositionChanged {e.Position} sender {sender}");
        }
    }
}
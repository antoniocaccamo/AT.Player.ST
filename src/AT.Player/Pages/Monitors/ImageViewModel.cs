using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Player.Pages.Monitors
{
    public class ImageViewModel : AbstractMonitorViewModel
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ImageViewModel(Stylet.IEventAggregator events) : base(events)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += (snd, evt) =>
            {
                var percentage = (int)(100 * DateTime.Now.Subtract(MonitorViewModel.CurrentMediaShowDateTime).TotalMilliseconds / MonitorViewModel.CurrentMedia.Duration.TotalMilliseconds);
                _logger.Info("{0} : percentage {1}", Channel, percentage);
                MonitorViewModel.FireProgressChanged(new ProgressChangedEventArgs(percentage, null));
                if (percentage >= 100)
                {
                    _timer.Stop();
                }
            };
        }

        public override Uri Source { get => base.Source; set { base.Source = value; _timer.Start(); } }
    }
}
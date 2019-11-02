using System;
using System.ComponentModel;

namespace AT.Player.Pages.Monitors
{
    public class FFMPEGVideoViewModel : AbstractMonitorViewModel
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Public Constructors

        public FFMPEGVideoViewModel(Stylet.IEventAggregator events) : base(events)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public void MediaElement_MediaEnded(object sender, EventArgs e)
        {
            _logger.Info($" {Channel} : media endend");
            this.MonitorViewModel.FireMediaEnded();
        }

        public void MediaElement_MediaFailed(object sender, Unosquare.FFME.Common.MediaFailedEventArgs e)
        {
            _logger.Error($"{Channel} : error playing video : {e.ErrorException}");
            this.MonitorViewModel.FireMediaEnded();
        }

        public void MediaElement_MediaOpened(object sender, Unosquare.FFME.Common.MediaOpenedEventArgs e)
        {
            _logger.Info($" {Channel} : media opened : {e.Info.MediaSource} Duration : {e.Info.Duration}");
            MonitorViewModel.CurrentMedia.Duration = e.Info.Duration;
        }

        public void MediaElement_PositionChanged(object sender, Unosquare.FFME.Common.PositionChangedEventArgs e)
        {
            var percentage = (int)(100 * e.Position.TotalMilliseconds / MonitorViewModel.CurrentMedia.Duration.TotalMilliseconds);
            _logger.Info($"{Channel} :  PositionChanged {e.Position} percentage {percentage}");
            MonitorViewModel.FireProgressChanged(new ProgressChangedEventArgs(percentage, null));
        }

        public override void Play()
        {
        }

        #endregion Public Methods
    }
}
using LibVLCSharp.Shared;
using Stylet;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AT.Player.Pages.Monitors
{
    public class VLCVideoViewModel : AbstractMonitorViewModel
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly LibVLC _libVLC;
        private readonly MediaPlayer _mediaPlayer;

        #endregion Private Fields

        #region Public Constructors

        public VLCVideoViewModel(Stylet.IEventAggregator events) : base(events)
        {
            //_timer.Tick += (snd, evt) =>
            //{
            //    var percentage = (int)(100 * _mediaElement?.Position.TotalMilliseconds / MonitorViewModel.CurrentMedia.Duration.TotalMilliseconds);
            //    _logger.Info($"{Channel} :  PositionChanged {_mediaElement?.Position} percentage {percentage}");
            //    MonitorViewModel.FireProgressChanged(new ProgressChangedEventArgs(percentage, null));
            //};

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            _mediaPlayer.EndReached += _mediaPlayer_EndReached;
            _mediaPlayer.Playing += _mediaPlayer_Playing;
            _mediaPlayer.PositionChanged += _mediaPlayer_PositionChanged;
            _mediaPlayer.EncounteredError += _mediaPlayer_EncounteredError;
        }

        #endregion Public Constructors

        #region Public Properties

        public MediaPlayer MediaPlayer => _mediaPlayer;

        public override Uri Source { get => base._source; set => base._source = value; }

        #endregion Public Properties

        #region Private Methods

        private void _mediaPlayer_EncounteredError(object sender, EventArgs e)
        {
            _logger.Error($"{Channel} : error playing video : {e}");
            MonitorViewModel.FireMediaEnded();
        }

        private void _mediaPlayer_EndReached(object sender, EventArgs e)
        {
            _logger.Info($" {Channel} : media endend : {e}");
            MonitorViewModel.FireMediaEnded();
        }

        private void _mediaPlayer_Playing(object sender, EventArgs e)
        {
            _logger.Info($" {Channel} : media opened : {e}");
            MonitorViewModel.CurrentMedia.Duration = TimeSpan.FromSeconds(6);
        }

        private void _mediaPlayer_PositionChanged(object sender, MediaPlayerPositionChangedEventArgs e)
        {
            var percentage = (int)(100 * e.Position / MonitorViewModel.CurrentMedia.Duration.TotalMilliseconds);
            _logger.Info($"{Channel} :  PositionChanged {e.Position} percentage {percentage}");
            MonitorViewModel.FireProgressChanged(new ProgressChangedEventArgs(percentage, null));
        }

        #endregion Private Methods

        public override void Play()
        {
            _mediaPlayer.Play(
                new Media(_libVLC,
                "C:\\Program Files\\WindowsApps\\Microsoft.Windows.Photos_2019.19071.17920.0_x64__8wekyb3d8bbwe\\AppCS\\Assets\\NewsControl_WhatsNewMedia\\620x252_3DModels.mp4"
)
            );
        }
    }
}
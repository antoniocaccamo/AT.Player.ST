using Stylet;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Vlc.DotNet.Wpf;

namespace AT.Player.Pages.Monitors
{
    public class VLCVideoViewModel : AbstractMonitorViewModel
    {
        #region Protected Fields

        protected VlcControl _vlcControl;

        #endregion Protected Fields

        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private MediaElement _mediaElement;

        #endregion Private Fields

        #region Public Constructors

        public VLCVideoViewModel(Stylet.IEventAggregator events) : base(events)
        {
            _timer.Tick += (snd, evt) =>
            {
                var percentage = (int)(100 * _mediaElement?.Position.TotalMilliseconds / MonitorViewModel.CurrentMedia.Duration.TotalMilliseconds);
                _logger.Info($"{Channel} :  PositionChanged {_mediaElement?.Position} percentage {percentage}");
                MonitorViewModel.FireProgressChanged(new ProgressChangedEventArgs(percentage, null));
            };
        }

        #endregion Public Constructors

        #region Public Properties

        public override Uri Source { get => base._source; set => base._source = value; }
        public VlcControl VlcControl { get => _vlcControl; set => _vlcControl = value; }

        #endregion Public Properties

        #region Public Methods

        public void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            _logger.Info($" {Channel} : media endend");
            FireMediaEnded();
        }

        //public VideoViewModel(string channel) : base(channel)
        //{
        //}
        public void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            _logger.Error($"{Channel} : error playing video : {e.ErrorException}");
            FireMediaEnded();
        }

        public void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            _logger.Info($" {Channel} : media opened ");
            _mediaElement = _mediaElement ?? (sender as MediaElement);
            this.MonitorViewModel.CurrentMedia.Duration = _mediaElement.NaturalDuration.TimeSpan;
            _timer.Start();
        }

        public void SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            var ar = $"{e.NewSize.Width}:{e.NewSize.Height}";
            _logger.Info("{0} : changing aspect ratio to {1}", Channel, ar);
            //_vlcControl.SourceProvider.MediaPlayer.Video.AspectRatio = ar;
        }

        #endregion Public Methods

        #region Protected Methods

        //public VlcVideoSourceProvider SourceProvider { get; }
        protected override void OnInitialActivate()
        {
            //base.OnInitialActivate();
            //_vlcControl = new VlcControl();
            //var vlcLibDirectory = new System.IO.DirectoryInfo(
            //        System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64")
            //    );

            //var options = new string[]
            //{
            //    // VLC options can be given here.
            //    // Please refer to the VLC command line documentation.
            //    "--file-logging",
            //    "-vvv",
            //    $"--logfile=logs/vlc.{Channel}.log"
            //};

            //_vlcControl.SourceProvider.CreatePlayer(vlcLibDirectory, options);

            //_vlcControl.SourceProvider.MediaPlayer.EncounteredError += (s, e) => _logger.Error("error occurred : {0}", e);
            //_vlcControl.SourceProvider.MediaPlayer.Playing += (s, e) => _logger.Info("{0} : playing {1}", Channel, e);
            //_vlcControl.SourceProvider.MediaPlayer.Stopped += (s, e) => _logger.Info("{0} : stopped {1}", Channel, e);
            //_vlcControl.SourceProvider.MediaPlayer.EndReached += (s, e) => _logger.Info("{0} : terminated {1}", Channel, e);
            //_vlcControl.SourceProvider.MediaPlayer.TimeChanged += (s, e) =>
            //{
            //    _logger.Info("{0} : time changed to {1}", Channel, TimeSpan.FromMilliseconds(e.NewTime).ToString(@"hh\:mm\:ss"));
            //    var progress = 100 * e.NewTime / _vlcControl.SourceProvider.MediaPlayer.Length;
            //    //Execute.PostToUIThread(() =>
            //    //    this._events.Publish(new Events.MediaProgressChangeEvent() { ProgressPercentage = (int)percentage }, Channel)
            //    //);

            //    var args = new ProgressChangedEventArgs((int)progress, null);
            //    MonitorViewModel.FireProgressChanged(args);
            //};
        }

        #endregion Protected Methods

        #region Private Methods

        private void FireMediaEnded()
        {
            _timer.Stop();
            this.MonitorViewModel.FireMediaEnded();
        }

        #endregion Private Methods

        //public void MediaElement_MediaFailed(object sender, Unosquare.FFME.Common.MediaFailedEventArgs e)
        //{
        //    _logger.Error($"{_channel} : error playing video : {e.ErrorException}");
        //}

        //public void MediaElement_MediaOpened(object sender, Unosquare.FFME.Common.MediaOpenedEventArgs e)
        //{
        //    _logger.Info($" {_channel} : media opened : {e.Info.MediaSource} Duration : {e.Info.Duration}");
        //}

        //public void MediaElement_MediaEnded(object sender, EventArgs e)
        //{
        //    _logger.Info($" {_channel} : media endend : sender  {sender}");
        //}

        //public void MediaElement_PositionChanged(object sender, Unosquare.FFME.Common.PositionChangedEventArgs e)
        //{
        //    _logger.Info($"{_channel} :  PositionChanged {e.Position} sender {sender}");
        //}
    }
}
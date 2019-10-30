using Stylet;
using System;
using System.ComponentModel;
using System.Windows;
using Vlc.DotNet.Wpf;

namespace AT.Player.Pages.Monitors
{
    public class VideoViewModel : AbstractMonitorViewModel
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        protected VlcControl _vlcControl;

        public VlcControl VlcControl { get => _vlcControl; set => _vlcControl = value; }

        //public VideoViewModel(string channel) : base(channel)
        //{
        //}

        public VideoViewModel(Stylet.IEventAggregator events) : base(events)
        {
        }

        public void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            _logger.Error($"{Channel} : error playing video : {e.ErrorException}");
        }

        public void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            _logger.Info($" {Channel} : media opened ");
        }

        public void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            _logger.Info($" {Channel} : media endend");
        }

        public VlcVideoSourceProvider SourceProvider { get; }

        public override Uri Source
        {
            get => _source; set
            {
                _source = value;
                _vlcControl.SourceProvider.MediaPlayer.SetMedia(value);
                _vlcControl.SourceProvider.MediaPlayer.Play();
            }
        }

        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();
            _vlcControl = new VlcControl();
            var vlcLibDirectory = new System.IO.DirectoryInfo(
                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64")
                );

            var options = new string[]
            {
                // VLC options can be given here.
                // Please refer to the VLC command line documentation.
                "--file-logging",
                "-vvv",
                $"--logfile=logs/vlc.{Channel}.log"
            };

            _vlcControl.SourceProvider.CreatePlayer(vlcLibDirectory, options);

            _vlcControl.SourceProvider.MediaPlayer.EncounteredError += (s, e) => _logger.Error("error occurred : {0}", e);
            _vlcControl.SourceProvider.MediaPlayer.Playing += (s, e) => _logger.Info("{0} : playing {1}", Channel, e);
            _vlcControl.SourceProvider.MediaPlayer.Stopped += (s, e) => _logger.Info("{0} : stopped {1}", Channel, e);
            _vlcControl.SourceProvider.MediaPlayer.EndReached += (s, e) => _logger.Info("{0} : terminated {1}", Channel, e);
            _vlcControl.SourceProvider.MediaPlayer.TimeChanged += (s, e) =>
            {
                _logger.Info("{0} : time changed to {1}", Channel, TimeSpan.FromMilliseconds(e.NewTime).ToString(@"hh\:mm\:ss"));
                var progress = 100 * e.NewTime / _vlcControl.SourceProvider.MediaPlayer.Length;
                //Execute.PostToUIThread(() =>
                //    this._events.Publish(new Events.MediaProgressChangeEvent() { ProgressPercentage = (int)percentage }, Channel)
                //);

                var args = new ProgressChangedEventArgs((int)progress, null);
                MonitorViewModel.FireProgressChanged(args);
            };
        }

        public void SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            var ar = $"{e.NewSize.Width}:{e.NewSize.Height}";
            _logger.Info("{0} : changing aspect ratio to {1}", Channel, ar);
            _vlcControl.SourceProvider.MediaPlayer.Video.AspectRatio = ar;
        }

        private Uri _source;

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
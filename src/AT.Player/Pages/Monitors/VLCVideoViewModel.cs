namespace AT.Player.Pages.Monitors
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using Vlc.DotNet.Core;
    using Vlc.DotNet.Forms;

    public class VLCVideoViewModel : AbstractMonitorViewModel
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly VlcControl _mediaPlayer;

        #endregion Private Fields

        #region Public Constructors

        public VLCVideoViewModel(Stylet.IEventAggregator events) : base(events)
        {
            _mediaPlayer = new Vlc.DotNet.Forms.VlcControl();

            // Default installation path of VideoLAN.LibVLC.Windows
            var libDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            _mediaPlayer.BeginInit();
            _mediaPlayer.VlcLibDirectory = libDirectory;
            _mediaPlayer.EndInit();

            _mediaPlayer.EndReached += _mediaPlayer_EndReached;
            _mediaPlayer.Playing += _mediaPlayer_Playing;
            _mediaPlayer.PositionChanged += _mediaPlayer_PositionChanged;
            _mediaPlayer.EncounteredError += _mediaPlayer_EncounteredError;
        }

        #endregion Public Constructors

        #region Public Properties

        public Vlc.DotNet.Forms.VlcControl MediaPlayer => _mediaPlayer;

        public override Uri Source { get => base._source; set => base._source = value; }

        #endregion Public Properties

        #region Public Methods

        public override void Play()
        {
            _mediaPlayer.Play(_source);
        }

        public void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.View as VLCVideoView).WindowsFormsHost.Child = (this.View as VLCVideoView).WindowsFormsHost.Child ?? _mediaPlayer;

            var size = this.MonitorViewModel.Monitor.Size;
            _mediaPlayer.Video.AspectRatio = $"{size.Width}:{size.Height}";
            _mediaPlayer.Width = (int)size.Width;
            _mediaPlayer.Height = (int)size.Height;
            _logger.Info("{0} : UserControl_Loaded : _mediaPlayer.Video.AspectRatio = {1}", Channel, _mediaPlayer.Video.AspectRatio);
        }

        public void UserControl_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            _mediaPlayer.Video.AspectRatio = $"{e.NewSize.Width}:{e.NewSize.Height}";
            _mediaPlayer.Width = (int)e.NewSize.Width;
            _mediaPlayer.Height = (int)e.NewSize.Height;
            _logger.Info("{0} : UserControl_SizeChanged : _mediaPlayer.Video.AspectRatio = {1}", Channel, _mediaPlayer.Video.AspectRatio);
        }

        #endregion Public Methods

        #region Private Methods

        private void _mediaPlayer_EncounteredError(object sender, VlcMediaPlayerEncounteredErrorEventArgs e)
        {
            _logger.Error($"{Channel} : error playing video : {e}");
            MonitorViewModel.FireMediaEnded();
        }

        private void _mediaPlayer_EndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            _logger.Info($" {Channel} : media endend : {e}");
            MonitorViewModel.FireMediaEnded();
        }

        private void _mediaPlayer_Playing(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            _logger.Info($" {Channel} : media opened : {e} _mediaPlayer.Length {_mediaPlayer.Length}");
            MonitorViewModel.CurrentMedia.Duration = TimeSpan.FromMilliseconds(_mediaPlayer.Length);
        }

        private void _mediaPlayer_PositionChanged(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            var percentage = (int)(100 * e.NewPosition);
            _logger.Info($"{Channel} :  PositionChanged {e.NewPosition} percentage {percentage}");
            MonitorViewModel.FireProgressChanged(new ProgressChangedEventArgs(percentage, null));
        }

        #endregion Private Methods

        protected override void OnActivate()
        {
            var size = this.MonitorViewModel.Monitor.Size;
            _mediaPlayer.Video.AspectRatio = $"{size.Width}:{size.Height}";
            _mediaPlayer.Width = (int)size.Width;
            _mediaPlayer.Height = (int)size.Height;
            _logger.Info("{0} : OnActivate : _mediaPlayer.Video.AspectRatio = {1}", Channel, _mediaPlayer.Video.AspectRatio);
        }
    }
}
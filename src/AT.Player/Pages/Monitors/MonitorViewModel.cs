namespace AT.Player.Pages.Monitors
{
    using AT.Player.Configuration;
    using AT.Player.Events;
    using Stylet;

    public class MonitorViewModel : Conductor<IScreen>.StackNavigation, IHandle<MonitorShowVideoEvent>, IHandle<MonitorShowImageEvent>
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        #region Private Fields

        private readonly string _channel;
        private readonly IEventAggregator _events;
        private readonly Monitor _monitor;
        private VideoViewModel _video;
        private ImageViewModel image;

        #endregion Private Fields

        #region Public Constructors

        public MonitorViewModel(IEventAggregator events, string channel, Monitor monitor)
        {
            this._events = events;
            this._channel = channel;
            this._monitor = monitor;
            DisplayName = channel;

            this._video = new VideoViewModel();
            this.image = new ImageViewModel();

            _events.Subscribe(this, channel);
        }

        #endregion Public Constructors

        #region Public Properties

        public double Height
        {
            get => _monitor.Size.Height;
            set => _monitor.Size.Height = value;
        }

        public double Left
        {
            get => _monitor.Location.Left;
            set => _monitor.Location.Left = value;
        }

        public double Top
        {
            get => _monitor.Location.Top;
            set => _monitor.Location.Top = value;
        }

        public double Width
        {
            get => _monitor.Size.Width;
            set => _monitor.Size.Width = value;
        }

        void IHandle<MonitorShowVideoEvent>.Handle(MonitorShowVideoEvent message)
        {
            _logger.Info("receveid msg {0}", message.Source);
            image.Source = message.Source;
            this.ActivateItem(image);
        }

        void IHandle<MonitorShowImageEvent>.Handle(MonitorShowImageEvent message)
        {
            _logger.Info("receveid msg {0}", message.Source);
            _video.Source = message.Source;
            this.ActivateItem(_video);
        }

        #endregion Public Properties
    }
}
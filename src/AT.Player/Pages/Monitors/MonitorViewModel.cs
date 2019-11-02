namespace AT.Player.Pages.Monitors
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using AT.Player.Configuration;
    using AT.Player.Events;
    using AT.Player.Model;
    using Stylet;

    /// <summary>
    /// </summary>
    public class MonitorViewModel : Conductor<IScreen>.StackNavigation, IHandle<MonitorShowMediaEvent>
    {
        #region Public Fields

        public event ProgressChangedEventHandler OnProgressChanged;

        #endregion Public Fields

        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IEventAggregator _events;
        private readonly BrowserViewModel _browser;
        private string _channel;
        private readonly ImageViewModel _image;
        private Monitor _monitor;
        private readonly VideoViewModel _video;

        private Media _media;
        private DateTime _currentMediaShowDateTiem;

        #endregion Private Fields

        public event OnMediaEnded OnMediaEnded;

        #region Public Constructors

        public MonitorViewModel(IEventAggregator events, VideoViewModel videoViewModel, ImageViewModel imageViewModel, BrowserViewModel browserViewModel)
        {
            this._events = events;
            this._video = videoViewModel;
            this._image = imageViewModel;
            this._browser = browserViewModel;
        }

        #endregion Public Constructors

        #region Public Properties

        public Monitor Monitor => _monitor;

        public Media CurrentMedia => _media;

        public DateTime CurrentMediaShowDateTime => _currentMediaShowDateTiem;

        #endregion Public Properties

        #region Public Methods

        public void ChannelAndMonitor(string channel, Monitor monitor)
        {
            this._channel = channel;
            this._monitor = monitor;
            DisplayName = channel;

            _browser.Channel = _channel;
            _image.Channel = _channel;
            _video.Channel = _channel;

            _browser.MonitorViewModel = this;
            _image.MonitorViewModel = this;
            _video.MonitorViewModel = this;

            _events.Subscribe(this, channel);
        }

        public void FireProgressChanged(ProgressChangedEventArgs evt)
        {
            Execute.PostToUIThread(() => OnProgressChanged(this, evt));
        }

        public void FireMediaEnded()
        {
            Execute.PostToUIThread(() => OnMediaEnded(this, new MediaEndedEvent() { Media = _media }));
        }

        void IHandle<MonitorShowMediaEvent>.Handle(MonitorShowMediaEvent evt)
        {
            _logger.Info("{0} : receveid MonitorShowMediaEvent : {1} {2}", _channel, evt.Media.Type, evt.Media.LocalFile);

            try
            {
                Uri source = null;
                //IScreen screen = null;
                AbstractMonitorViewModel @abstract = null;
                switch (evt.Media.Type)
                {
                    case Media.MediaTypeEnum.VIDEO:
                        @abstract = _video;
                        source = new Uri(evt.Media.LocalFile);
                        //Execute.PostToUIThread(() => _video.Source = source);
                        //;                       // _video.Source = source;
                        //this.ActivateItem(_video);
                        break;

                    case Media.MediaTypeEnum.IMAGE:
                        @abstract = _image;
                        source = new Uri(evt.Media.LocalFile);
                        //_image.Source = source;
                        //this.ActivateItem(_image);
                        break;
                }
                @abstract.Source = source;
                @abstract.Play();
                this.ActivateItem(@abstract);
                _media = evt.Media;
                _currentMediaShowDateTiem = DateTime.Now;
            }
            catch (Exception e)
            {
                _logger.Error(e, "error occurred");
                this._events.Publish(new MonitorShowMediaErrorEvent(evt.Media), _channel);
            }
        }

        #endregion Public Methods

        //public double Height
        //{
        //    get => _monitor.Size.Height;
        //    set => _monitor.Size.Height = value;
        //}

        //public double Left
        //{
        //    get => _monitor.Location.Left;
        //    set => _monitor.Location.Left = value;
        //}

        //public double Top
        //{
        //    get => _monitor.Location.Top;
        //    set => _monitor.Location.Top = value;
        //}

        //public double Width
        //{
        //    get => _monitor.Size.Width;
        //    set => _monitor.Size.Width = value;
        //}
    }
}
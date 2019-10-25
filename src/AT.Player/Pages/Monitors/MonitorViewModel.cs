namespace AT.Player.Pages.Monitors
{
    using System;
    using System.ComponentModel;
    using AT.Player.Configuration;
    using AT.Player.Events;
    using Stylet;

    /// <summary>
    /// </summary>
    public class MonitorViewModel : Conductor<IScreen>.StackNavigation, IHandle<MonitorShowMediaEvent>
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly string _channel;
        private readonly IEventAggregator _events;
        private readonly Monitor _monitor;
        private ImageViewModel _image;
        private VideoViewModel _video;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="events"></param>
        /// <param name="channel"></param>
        /// <param name="monitor"></param>
        public MonitorViewModel(IEventAggregator events, string channel, Monitor monitor)
        {
            this._events = events;
            this._channel = channel;
            this._monitor = monitor;
            DisplayName = channel;

            this._video = new VideoViewModel(_channel);
            this._image = new ImageViewModel(_channel);

            _events.Subscribe(this, channel);
        }

        internal void FireProgressChanged(ProgressChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        #endregion Public Constructors

        #region Public Properties

        public Configuration.Monitor Monitor => _monitor;

        internal void RequestNext()
        {
            throw new NotImplementedException();
        }

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

        #endregion Public Properties

        #region Public Methods

        void IHandle<MonitorShowMediaEvent>.Handle(MonitorShowMediaEvent evt)
        {
            _logger.Info("{0} : receveid MonitorShowMediaEvent : {1}", _channel, evt);

            try
            {
                Uri source;
                //IScreen screen = null;
                switch (evt.Media.Type)
                {
                    case Model.Media.MediaTypeEnum.VIDEO:
                        source = new Uri(evt.Media.LocalFile);
                        _video.Source = source;
                        this.ActivateItem(_video);
                        break;

                    case Model.Media.MediaTypeEnum.IMAGE:
                        source = new Uri(evt.Media.LocalFile);
                        _image.Source = source;
                        this.ActivateItem(_image);
                        break;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "error occurred");
                this._events.Publish(new MonitorShowMediaErrorEvent(evt.Media), _channel);
            }
        }

        #endregion Public Methods
    }
}
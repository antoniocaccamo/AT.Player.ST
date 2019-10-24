using AT.Player.Model;
using AT.Player.Pages.Monitors;
using System;
using System.ComponentModel;
using System.Threading;

namespace AT.Player.Callbacks
{
    internal class MonitorTimingCallback : ITimingCallback
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly MonitorViewModel _pvm;
        private readonly double _duration;
        private readonly DateTime _startup;
        private readonly Media _media;

        #endregion Private Fields

        #region Public Constructors

        public MonitorTimingCallback(MonitorViewModel pvm, Media media)
        {
            _pvm = pvm;
            _media = media;
            _startup = DateTime.Now;
        }

        #endregion Public Constructors

        #region Public Methods

        public void timingCallBack(object obj)
        {
            TimeSpan diff = DateTime.Now.Subtract(_startup);
            ProgressChangedEventArgs args = null;
            if (diff.TotalSeconds < (int)_media.Duration)
            {
                int progress = (int)(100 * diff.TotalSeconds / _media.Duration);
                args = new ProgressChangedEventArgs(progress, null);
                _pvm.FireProgressChanged(args);
            }
            else
            {
                args = new ProgressChangedEventArgs(100, null);
                _pvm.FireProgressChanged(args);

                AutoResetEvent autoEvent = (AutoResetEvent)obj;
                autoEvent.Set();
                if (!_media.isVideo)
                {
                    _pvm.RequestNext();
                }
            }
        }

        public TimeSpan Duration => TimeSpan.FromMilliseconds(1000 * _duration);

        #endregion Public Methods
    }
}
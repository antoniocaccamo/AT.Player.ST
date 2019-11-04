namespace AT.Player.Model
{
    using System;
    using YamlDotNet.Serialization;

    [ToString]
    public class Media : IModel
    {
        #region Private Fields

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Public Enums

        public enum LocationTypeEnum
        {
            LOCAL,
            FTP,
            WEB,
            // S3
        }

        public enum MediaTypeEnum
        {
            BLACK,
            HIDDEN,
            WATCH,
            IMAGE,
            VIDEO,
            BROWSER,
            WEATHER
        }

        #endregion Public Enums

        #region Public Properties

        public long Cicles { get; set; }

        public Dated Dated { get; set; }

        public bool[] Days { get; set; }

        public TimeSpan Duration { get; set; }

        [YamlIgnore] public bool isVideo => MediaTypeEnum.VIDEO.Equals(Type);
        public string LocalFile { get; set; }

        public LocationTypeEnum Location { get; set; }
        public string RemoteFile { get; set; }

        public Timed Timed { get; set; }

        public MediaTypeEnum Type { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void PostConstruct()
        {
            _logger.Info($"postConstruct {LocalFile} ...");
        }

        #endregion Public Methods

        #region Internal Methods

        internal bool IsPlayable(DateTime now)
        {
            return true;
        }

        #endregion Internal Methods
    }
}
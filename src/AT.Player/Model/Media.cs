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

        #region Properties

        public long Cicles { get; set; }

        public Dated Dated { get; set; }

        public bool[] Days { get; set; }

        public TimeSpan Duration { get; set; }

        public string LocalFile { get; set; }

        public string RemoteFile { get; set; }

        public Timed Timed { get; set; }

        public MediaTypeEnum Type { get; set; }

        public LocationTypeEnum Location { get; set; }

        public void PostConstruct()
        {
            _logger.Info($"postConstruct {LocalFile} ...");
        }

        internal bool IsPlayable(DateTime now)
        {
            return true;
        }

        #endregion Properties

        #region Public Properties

        [YamlIgnore] public bool isVideo => MediaTypeEnum.VIDEO.Equals(Type);

        #endregion Public Properties

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

        public enum LocationTypeEnum
        {
            LOCAL,
            FTP,
            WEB,
            // S3
        }
    }
}
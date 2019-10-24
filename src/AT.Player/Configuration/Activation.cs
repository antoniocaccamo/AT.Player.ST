namespace AT.Player.Configuration
{
    using System;

    [ToString]
    public class Activation //: Stylet.PropertyChangedBase
    {
        #region Public Enums

        public enum ActivationEnum
        {
            ALLDAY,
            TIMED
        }

        public enum WhenNotActiveEnum
        {
            BLACK,
            IMAGE,
            WATCH
        }

        #endregion Public Enums

        #region Public Properties

        public string Image { get; set; }
        public ActivationEnum Type { get; set; }

        public WhenNotActiveEnum WhenNotActive { get; set; }

        #endregion Public Properties

        #region Private Properties

        private TimeSpan From { get; set; }

        private TimeSpan To { get; set; }

        #endregion Private Properties
    }
}
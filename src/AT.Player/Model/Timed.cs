namespace AT.Player.Model
{
    using System;

    [ToString]
    public class Timed
    {
        #region Properties

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        #endregion Properties
    }
}
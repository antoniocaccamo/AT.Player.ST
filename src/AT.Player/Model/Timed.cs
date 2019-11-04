namespace AT.Player.Model
{
    using Stylet;
    using System;

    [ToString]
    public class Timed : PropertyChangedBase
    {
        #region Properties

        public TimeSpan From { get; set; }

        public TimeSpan To { get; set; }

        #endregion Properties
    }
}
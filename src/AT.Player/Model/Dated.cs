namespace AT.Player.Model
{
    using Stylet;
    using System;

    [ToString]
    public class Dated : PropertyChangedBase
    {
        #region Properties

        public DateTime End { get; set; }

        public DateTime Start { get; set; }

        #endregion Properties
    }
}
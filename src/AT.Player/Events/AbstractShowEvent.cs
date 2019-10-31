namespace AT.Player.Events
{
    using AT.Player.Model;
    using System;

    public abstract class AbstractShowEvent : EventArgs
    {
        #region Private Fields

        private Media _media;

        #endregion Private Fields

        #region Public Properties

        public virtual Media Media { get => _media; set => _media = value; }
        public virtual Uri Source { get; set; }

        #endregion Public Properties
    }
}
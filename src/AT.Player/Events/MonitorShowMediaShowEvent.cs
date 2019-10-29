namespace AT.Player.Events
{
    using AT.Player.Model;

    [ToString]
    internal class MonitorShowMediaShowEvent : AbstractShowEvent
    {
        #region Private Fields

        private readonly Media _media;

        #endregion Private Fields

        #region Public Constructors

        public MonitorShowMediaShowEvent(Media media)
        {
            _media = media;
        }

        #endregion Public Constructors

        #region Public Properties

        public Media Media => _media;

        #endregion Public Properties
    }
}
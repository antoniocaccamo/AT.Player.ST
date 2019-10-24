namespace AT.Player.Configuration
{
    [ToString]
    public class Monitor //: Stylet.PropertyChangedBase

    {
        #region Public Properties

        public Size Size { get; set; }

        public Location Location { get; set; }

        public Activation Activation { get; set; }

        public string Sequence { get; set; }

        #endregion Public Properties
    }
}
namespace AT.Player.Configuration
{
    public class Monitor

    {
        #region Public Properties

        public Size Size { get; set; }

        public Location Location { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return $"size [{Size}] location [{Location}]";
        }

        #endregion Public Methods
    }
}
namespace AT.Player.Configuration
{
    public class Monitor : Stylet.PropertyChangedBase

    {
        #region Public Properties

        public Size Size { get; set; }

        public Location Location { get; set; }

        public Activation Activation { get; set; }

        public string Sequence { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            var ret = $"size [{Size}] location [{Location}] sequence [{Sequence}] activation [{Activation}]";
            return ret;
        }

        #endregion Public Methods
    }
}
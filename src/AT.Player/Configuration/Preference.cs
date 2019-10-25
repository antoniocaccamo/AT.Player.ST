namespace AT.Player.Configuration
{
    using Stylet;

    [ToString]
    public class Preference
    {
        #region Public Properties

        public string Computer { get; set; }

        public Size Size { get; set; }

        public Location Location { get; set; }

        //public Dummy Dummy { get; set; }

        public BindableCollection<Monitor> Monitors { get; set; }

        #endregion Public Properties
    }
}
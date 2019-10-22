using System.Collections.Generic;

namespace AT.Player.Configuration
{
    public class Configuration
    {
        #region Public Properties

        public string Computer { get; set; }

        public Size Size { get; set; }

        public Location Location { get; set; }

        public Dummy Dummy { get; set; }

        public IEnumerable<Monitor> Monitors { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return $"dummy : [{Dummy}]\n"
                + $"\tcomputer : [{Computer}]\n"
                + $"\tsize [{Size}]\n"
                + $"\tlocation [{Location}]\n"
                + $"\tmonitors [{Monitors}]\n"; ;
        }

        #endregion Public Methods
    }
}
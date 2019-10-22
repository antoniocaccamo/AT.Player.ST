using Stylet;
using System.Collections.Generic;

namespace AT.Player.Configuration
{
    public class Preference
    {
        #region Public Properties

        public string Computer { get; set; }

        public Size Size { get; set; }

        public Location Location { get; set; }

        public Dummy Dummy { get; set; }

        public BindableCollection<Monitor> Monitors { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            var ret = $"dummy : [{Dummy}]\n"
                + $"\tcomputer : [{Computer}]\n"
                + $"\tsize [{Size}]\n"
                + $"\tlocation [{Location}]\n"
                + $"\tmonitors [{Monitors}]\n"; ;
            return ret;
        }

        #endregion Public Methods
    }
}
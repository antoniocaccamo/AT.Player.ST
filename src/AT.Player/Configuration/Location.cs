/// <summary>
///
/// </summary>
namespace AT.Player.Configuration
{
    public class Location : Stylet.PropertyChangedBase
    {
        //public Location(double l, double t)
        //{
        //    Left = l;
        //    Top = t;
        //}

        #region Public Properties

        public double Top { get; set; }

        public double Left { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return $"Top [{Top}] Left [{Left}]";
        }

        #endregion Public Methods
    }
}
/// <summary>
///
/// </summary>
namespace AT.Player.Configuration
{
    [ToString]
    public class Location //: Stylet.PropertyChangedBase
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
    }
}
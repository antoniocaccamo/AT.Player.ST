/// <summary>
///
/// </summary>
namespace AT.Player.Configuration
{
    public class Size
    {
        #region Public Properties

        public double Width { get; set; }

        public double Height { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return $"Width [{Width}] Height [{Height}]";
        }

        #endregion Public Methods
    }
}
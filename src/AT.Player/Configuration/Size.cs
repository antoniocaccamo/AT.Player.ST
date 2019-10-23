/// <summary>
///
/// </summary>
namespace AT.Player.Configuration
{
    using Stylet;

    public class Size : PropertyChangedBase
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        #region Public Properties

        private double _width;

        public double Width
        {
            get => _width;
            set
            {
                _logger.Warn("width changed {0}", value);
                SetAndNotify(ref this._width, value);
            }
        }

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
using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Position
    {
        #region Public Properties

        public double Top { get; set; }

        public double Left { get; set; }

        public override string ToString ( )
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion Public Properties
    }
}
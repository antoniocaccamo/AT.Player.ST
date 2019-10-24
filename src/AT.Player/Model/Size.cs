using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Size
    {
        #region Public Properties

        public double Height { get; set; }
        public double Width { get; set; }

        public override string ToString ( )
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion Public Properties
    }
}
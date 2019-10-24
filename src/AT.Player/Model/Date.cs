using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Date
    {
        #region Properties

        [JsonProperty("Color")]
        public Color Color { get; set; }

        [JsonProperty("Font")]
        public Font Font { get; set; }

        #endregion Properties
    }
}
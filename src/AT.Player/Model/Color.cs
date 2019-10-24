using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Color
    {
        #region Properties

        [JsonProperty("ToArgb")]
        public long ToArgb { get; set; }

        #endregion Properties
    }
}
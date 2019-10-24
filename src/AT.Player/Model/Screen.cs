using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Screen
    {
        #region Properties

        [JsonProperty("Fade")]
        public bool Fade { get; set; }

        [JsonProperty("Lock")]
        public bool Lock { get; set; }

        [JsonProperty("View")]
        public bool View { get; set; }

        #endregion Properties
    }
}
using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Watch
    {
        #region Properties

        [JsonProperty("Date")]
        public Date Date { get; set; }

        [JsonProperty("ImageFile")]
        public string ImageFile { get; set; }
        [JsonProperty("Time")]
        public Date Time { get; set; }

        #endregion Properties
    }
}
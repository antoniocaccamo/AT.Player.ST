using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Weather
    {
        #region Properties

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        #endregion Properties
    }
}
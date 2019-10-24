using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Activation
    {
        #region Properties

        [JsonProperty("AllDay")]
        public bool AllDay { get; set; }

        [JsonProperty("Timed")]
        public Timed Timed { get; set; }

        #endregion Properties

        #region Public Methods

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Services.PalimpsestService.Converter.Settings);
        }

        #endregion Public Methods
    }
}
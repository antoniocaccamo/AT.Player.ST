using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Font
    {
        #region Properties

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Size")]
        public long Size { get; set; }

        #endregion Properties
    }
}
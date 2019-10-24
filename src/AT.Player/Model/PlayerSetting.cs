namespace AT.Player.Domain
{
    using AT.Player.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class PlayerSetting : IModel
    {
        #region Properties

        [JsonProperty("ComputerName")]
        public string ComputerName { get; set; }

        [JsonProperty("FtpRefreh")]
        public double FtpRefreh { get; set; }

        [JsonProperty("Locale")]
        public string Locale { get; set; }

        [JsonProperty("LogEnabled")]
        public bool LogEnabled { get; set; }

        [JsonProperty("Rectangle")]
        public long[] Rectangle { get; set; }

        [JsonProperty("SendAllMail")]
        public bool SendAllMail { get; set; }

        [JsonProperty("Settings")]
        public List<Setting> Settings { get; set; }

        [JsonProperty("Weather")]
        public Weather Weather { get; set; }

        [JsonProperty("Size")]
        public Size Size { get; set; }

        [JsonProperty("Position")]
        public Position Position { get; set; }

        public void PostConstruct()
        {
            Settings.ForEach(s => s.PostConstruct());
        }

        #endregion Properties

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
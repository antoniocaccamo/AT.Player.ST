using AT.Player.Domain.Interfaces;
using AT.Player.Enum;
using Newtonsoft.Json;

namespace AT.Player.Domain
{
    public class Setting : IModel
    {
        #region Properties

        [JsonProperty("Activation")]
        public Activation Activation { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonIgnore]
        public bool IsSelected { get; set; }

        [JsonProperty("Loops")]
        public long Loops { get; set; }

        [JsonProperty("Palimpsest")]
        public string Palimpsest { get; set; }

        [JsonProperty("Position")]
        public Position Position { get; set; }

        [JsonProperty("Rectangle")]
        public long[] Rectangle { get; set; }

        [JsonProperty("Screen")]
        public Screen Screen { get; set; }

        [JsonProperty("Size")]
        public Size Size { get; set; }

        [JsonProperty("Watch")]
        public Watch Watch { get; set; }

        [JsonIgnore]
        public SettingStatusEnum Status { get; set; }

        [JsonIgnore]
        public Media CurrentMedia { get; set; }

        [JsonIgnore]
        public PalimpsestLooper PalimpsestLooper { get; set; }

        public void PostConstruct()
        {
            Status = SettingStatusEnum.NOT_ACTIVE;
        }

        #endregion Properties

        #region Public Methods

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Services.PalimpsestService.Converter.Settings);
        }

        #endregion Public Methods
    }
}
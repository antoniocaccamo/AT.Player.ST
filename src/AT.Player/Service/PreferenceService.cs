namespace AT.Player.Service
{
    using System;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    internal class PreferenceService : IPreferenceService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string PREF_FILE = @"Resources/prefs.yml";

        private readonly IDeserializer _deserializer;

        public PreferenceService()
        {
            _deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                        .IgnoreUnmatchedProperties()
                                        .Build();
        }

        Configuration.Preference IPreferenceService.Get()
        {
            string yaml = System.IO.File.ReadAllText(PREF_FILE);
            _logger.Info($"yaml : ${yaml}");
            var configuration = _deserializer.Deserialize<Configuration.Preference>(yaml);
            return configuration;
        }

        void IPreferenceService.Save(Configuration.Preference configuration)
        {
            throw new NotImplementedException();
        }
    }
}
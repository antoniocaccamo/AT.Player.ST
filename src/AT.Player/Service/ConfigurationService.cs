namespace AT.Player.Service
{
    using System;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    internal class ConfigurationService : IConfigurationService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IDeserializer _deserializer;

        public ConfigurationService()
        {
            _deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(new CamelCaseNamingConvention())
                                        .IgnoreUnmatchedProperties()
                                        .Build();
        }

        Configuration.Configuration IConfigurationService.GetConfiguration()
        {
            string yaml = System.IO.File.ReadAllText(@"prefs.yml");
            _logger.Info($"yaml : ${yaml}");
            var configuration = _deserializer.Deserialize<Configuration.Configuration>(yaml);
            return configuration;
        }

        void IConfigurationService.SaveConfiguration(Configuration.Configuration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
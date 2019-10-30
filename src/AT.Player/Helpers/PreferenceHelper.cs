using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AT.Player.Helpers
{
    internal class PreferenceHelper
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string PREF_FILE = @"Resources/prefs.yaml";

        private static readonly ISerializer _serializer = new SerializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                .Build();

        private static readonly IDeserializer _deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                        .IgnoreUnmatchedProperties()
                                        .Build();

        public static Configuration.Preference Get()
        {
            string yaml = System.IO.File.ReadAllText(PREF_FILE);
            _logger.Debug("yaml : {0}", yaml);
            var configuration = _deserializer.Deserialize<Configuration.Preference>(yaml);
            return configuration;
        }

        public static void Save(Configuration.Preference configuration)
        {
            var yaml = _serializer.Serialize(configuration);
            _logger.Info("yaml : {0}", yaml);
        }
    }
}
using AT.Player.Model;
using System;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AT.Player.Helpers
{
    internal class PalimpsestHelper
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly ISerializer _serializer = new SerializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                .Build();

        private static readonly IDeserializer _deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                        .IgnoreUnmatchedProperties()
                                        .Build();

        public static Palimpsest Get(string file)
        {
            try
            {
                if (string.IsNullOrEmpty(file)) return null;

                if (System.IO.File.Exists(file))
                {
                    string yaml = System.IO.File.ReadAllText(file);
                    _logger.Debug("yaml : {0}", yaml);
                    var palimpsest = _deserializer.Deserialize<Palimpsest>(yaml);
                    return palimpsest;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "error occurred");
            }
            return null;
        }

        public static async Task<Palimpsest> GetAsync(string file) => await Task.Run(() => Get(file));

        public static bool Save(Palimpsest palimpsest)
        {
            return false;
        }

        public static Task<bool> SaveAsync(Palimpsest palimpsest) => Task.Run(() => Save(palimpsest));
    }
}
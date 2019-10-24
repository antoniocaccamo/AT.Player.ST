using System;

namespace AT.Player.Service
{
    using AT.Player.Model;
    using System.Threading.Tasks;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    internal class PalimpsestService : IPalimpsestService
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly ISerializer _serializer = new SerializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                .Build();

        private static readonly IDeserializer _deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                        .IgnoreUnmatchedProperties()
                                        .Build();

        public Palimpsest Get(string file)
        {
            try
            {
                if (System.IO.File.Exists(file))
                {
                    string yaml = System.IO.File.ReadAllText(file);
                    _logger.Info("yaml : {0}", yaml);
                    var palimpsest = _deserializer.Deserialize<Palimpsest>(yaml);
                    return palimpsest;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "error occurred");
            }
            return new Palimpsest();
        }

        public Task<Palimpsest> GetAsync(string file) => Task.Run(() => Get(file));

        public bool Save(Palimpsest palimpsest)
        {
            return false;
        }

        public Task<bool> SaveAsync(Palimpsest palimpsest) => Task.Run(() => Save(palimpsest));
    }
}
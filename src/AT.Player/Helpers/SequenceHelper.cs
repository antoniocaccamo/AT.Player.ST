using AT.Player.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AT.Player.Helpers
{
    internal class SequenceHelper
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly ISerializer _serializer = new SerializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                .Build();

        private static readonly IDeserializer _deserializer = new DeserializerBuilder()
                                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                        .IgnoreUnmatchedProperties()
                                        .Build();

        private static IDictionary<string, Sequence> _sequencesLoaded = new Dictionary<string, Sequence>();

        public static Sequence Get(string file)
        {
            try
            {
                if (string.IsNullOrEmpty(file)) return null;

                if (System.IO.File.Exists(file))
                {
                    string yaml = System.IO.File.ReadAllText(file);
                    _logger.Debug("yaml : {0}", yaml);
                    var sequence = _deserializer.Deserialize<Sequence>(yaml);
                    sequence.PostConstruct();
                    if (!_sequencesLoaded.ContainsKey(file))
                    {
                        _sequencesLoaded.Add(file, sequence);
                    }
                    return sequence;
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "error occurred");
            }
            return null;
        }

        public static async Task<Sequence> GetAsync(string file) => await Task.Run(() => Get(file));

        public static bool Save(Sequence palimpsest)
        {
            return false;
        }

        public static IDictionary<string, Sequence> SequencesLoaded => _sequencesLoaded;

        public static Task<bool> SaveAsync(Sequence palimpsest) => Task.Run(() => Save(palimpsest));
    }
}
using System.IO;
using Titan.Core.Logging;

namespace Titan.Core.Assets.Angelfont
{
    internal class AngelfontLoader : IAngelfontLoader
    {
        private readonly IAngelfontParser _parser;
        private readonly ILogger _logger;

        public AngelfontLoader(IAngelfontParser parser, ILogger logger)
        {
            _parser = parser;
            _logger = logger;
        }

        public Angelfont LoadFromPath(string path)
        {
            _logger.Debug("Loading Angelfont from {0}", path);
            using var file = File.OpenText(path);
            var result = _parser.ParseFromStream(file);
            _logger.Debug("Loaded Angelfont '{0}' with {1} characters", result.Info.Face, result.Characters.Length);
            return result;
        }
    }
}

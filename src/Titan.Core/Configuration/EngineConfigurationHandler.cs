using System;
using System.IO;
using Titan.Core.Common;
using Titan.Core.Json;

namespace Titan.Core.Configuration
{
    internal class EngineConfigurationHandler : IEngineConfigurationHandler
    {
        private readonly IEnginePaths _enginePaths;
        private readonly IFileReader _fileReader;
        private readonly IJsonSerializer _jsonSerializer;

        public EngineConfigurationHandler(IEnginePaths enginePaths, IFileReader fileReader, IJsonSerializer jsonSerializer)
        {
            _enginePaths = enginePaths;
            _fileReader = fileReader;
            _jsonSerializer = jsonSerializer;
        }

        public IConfiguration LoadConfiguration()
        {
            var rendererConfig = LoadConfig<RendererConfiguration>("renderer.json");
            var soundConfig = LoadConfig<SoundConfiguration>("sound.json");
            var engineConfig = LoadConfig<EngineConfiguration>("engine.json");

            return new Configuration
            {
                Debug = engineConfig.Debug,
                FixedUpdateFrequency = engineConfig.FixedUpdateFrequency,
                Height = engineConfig.Height,
                Width = engineConfig.Width,
                Title = engineConfig.Title,
                Renderer = rendererConfig,
                Sound = soundConfig,
                Paths = new ResourcePaths
                {
                    Base = _enginePaths.ResourcesPath,
                    Fonts = Path.Combine(_enginePaths.ResourcesPath, "fonts"),
                    Shaders = Path.Combine(_enginePaths.ResourcesPath, "shaders"),
                    Sounds = Path.Combine(_enginePaths.ResourcesPath, "sounds")
                }
            };
        }

        private T LoadConfig<T>(string filename)
        {
            var path = Path.Combine(_enginePaths.ConfigurationPath, filename);
            var contents = _fileReader.ReadAsString(path);
            if (string.IsNullOrWhiteSpace(contents))
            {
                throw new InvalidOperationException($"Fail to load {typeof(T).Name} from path {path}");
            }
            return _jsonSerializer.Deserialize<T>(contents);
        }
    }
}

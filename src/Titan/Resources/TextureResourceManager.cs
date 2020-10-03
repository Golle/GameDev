using System.Diagnostics;
using System.IO;
using Titan.Components;
using Titan.Core.Configuration;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    internal class TextureResourceManager : ResourceManager<string, ITexture2D>
    {
        private readonly ITextureLoader _textureLoader;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public TextureResourceManager(ITextureLoader textureLoader, IConfiguration configuration, ILogger logger)
        {
            _textureLoader = textureLoader;
            _configuration = configuration;
            _logger = logger;
        }

        protected override ITexture2D Load(in string identifier)
        {
            var filename = Path.Combine(_configuration.Paths.Base, identifier);
            var timer = Stopwatch.StartNew();
            var texture = _textureLoader.LoadTexture(filename);
            timer.Stop();
            _logger.Debug("Texture: {0} loaded in {1} ms", identifier, timer.Elapsed.TotalMilliseconds);
            return texture;
        }

        protected override void Unload(in string identifier, ITexture2D resource)
        {
            _logger.Debug("Texture: {0} unloaded", identifier);
            resource.Dispose();
        }

        protected override void OnLoaded(Entity entity, in string identifier, ITexture2D resource)
        {
            entity.AddComponent(new Texture2D{Texture = resource});
        }
    }
}

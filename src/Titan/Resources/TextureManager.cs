using System.Diagnostics;
using Titan.Components;
using Titan.Core.Logging;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    internal class TextureManager : ResourceManager<string, ITexture2D>
    {
        private readonly ITextureLoader _textureLoader;
        private readonly ILogger _logger;

        public TextureManager(ITextureLoader textureLoader, ILogger logger)
        {
            _textureLoader = textureLoader;
            _logger = logger;
        }

        protected override ITexture2D Load(in string identifier)
        {
            var timer = Stopwatch.StartNew();
            var texture = _textureLoader.LoadTexture(identifier);
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

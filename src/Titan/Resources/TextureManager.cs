using System;
using Titan.Components;
using Titan.ECS.Entities;
using Titan.ECS.Systems;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    internal class TextureManager : ResourceManager<string, ITexture2D>
    {
        private readonly ITextureLoader _textureLoader;
        
        public TextureManager(ITextureLoader textureLoader)
        {
            _textureLoader = textureLoader;
        }

        protected override ITexture2D Load(in string identifier)
        {
            Console.WriteLine($"Texture: {identifier} loaded");
            return _textureLoader.LoadTexture(identifier);
        }

        protected override void Unload(ITexture2D resource)
        {
            Console.WriteLine($"Texture unloaded");
            resource.Dispose();
        }

        protected override void OnLoaded(Entity entity, ITexture2D resource)
        {
            entity.AddComponent(new Texture2D{Texture = resource});
        }
    }
}

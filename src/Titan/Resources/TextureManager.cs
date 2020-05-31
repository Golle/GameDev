using System;
using System.Collections.Generic;
using Titan.Graphics;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    internal class TextureManager : ITextureManager
    {
        private readonly IDevice _device;
        private readonly IImageLoader _imageLoader;

        private readonly IDictionary<string, ITexture2D> _loadedTextures = new Dictionary<string, ITexture2D>();
        public TextureManager(IDevice device, IImageLoader imageLoader)
        {
            _device = device;
            _imageLoader = imageLoader;
        }

        public ITexture2D LoadTexture(string filename)
        {
            if (_loadedTextures.TryGetValue(filename, out var texture))
            {
                return texture;
            }
            
            var image = _imageLoader.LoadFromFile(filename);
            return _loadedTextures[filename] = _device.CreateTexture2D(image.Width, image.Height, image.Pixels);
        }

        public void ReleaseTexture(string filename)
        {
            if (_loadedTextures.Remove(filename, out var texture))
            {
                texture.Dispose();
            }
        }

        public void ReleaseAll()
        {
            foreach (var texture in _loadedTextures.Values)
            {
                texture.Dispose();
            }
            _loadedTextures.Clear();
        }
    }
}

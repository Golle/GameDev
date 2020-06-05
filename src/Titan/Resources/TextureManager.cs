using System.Collections.Generic;
using Titan.Graphics.Textures;

namespace Titan.Resources
{
    internal class TextureManager : ITextureManager
    {
        private readonly ITextureLoader _textureLoader;
        private readonly IDictionary<string, ITexture2D> _loadedTextures = new Dictionary<string, ITexture2D>();
        
        public TextureManager(ITextureLoader textureLoader)
        {
            _textureLoader = textureLoader;
        }

        public ITexture2D GetTexture(string filename)
        {
            if (_loadedTextures.TryGetValue(filename, out var texture))
            {
                return texture;
            }
            return _loadedTextures[filename] = _textureLoader.LoadTexture(filename);
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

        public void Dispose()
        {
            ReleaseAll();
        }
    }
}

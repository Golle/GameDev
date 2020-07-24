using System;
using Titan.Graphics.Textures;

namespace Titan.Graphics.Fonts
{
    public interface IFont : IDisposable
    {
        ITexture2D Texture { get; }
        ref readonly TextureCoordinates Get(char character);
    }

    internal class Font : IFont
    {
        public ITexture2D Texture { get; }
        public ref readonly TextureCoordinates Get(char character)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
